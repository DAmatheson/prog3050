﻿/* CartController.cs
 * Purpose: Controller for cart related actions
 * 
 * Revision History:
 *      Drew Matheson, 2015.10.13: Created
 */ 

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Veil.DataAccess.Interfaces;
using Veil.DataModels;
using Veil.DataModels.Models;
using Veil.Helpers;
using Veil.Models;
using Veil.Services.Interfaces;

namespace Veil.Controllers
{
    /// <summary>
    ///     Controller for actions related to Cart
    /// </summary>
    [Authorize(Roles = VeilRoles.MEMBER_ROLE)]
    public class CartController : BaseController
    {
        private readonly IVeilDataAccess db;
        private readonly IGuidUserIdGetter idGetter;
        private readonly IShippingCostService shippingCostService;

        public const string CART_QTY_SESSION_KEY = "Session.Cart.Quantity";

        /// <summary>
        ///     Instantiates a new instance of CartController with the provided arguments
        /// </summary>
        /// <param name="veilDataAccess">
        ///     The <see cref="IVeilDataAccess"/> to use for database access
        /// </param>
        /// <param name="idGetter">
        ///     The <see cref="IGuidUserIdGetter"/> to use for getting the current user's Id
        /// </param>
        /// <param name="shippingCostService">
        ///     The <see cref="IShippingCostService"/> to use for getting the shipping cost for
        ///     the items in the cart
        /// </param>
        public CartController(
            IVeilDataAccess veilDataAccess, IGuidUserIdGetter idGetter,
            IShippingCostService shippingCostService)
        {
            db = veilDataAccess;
            this.idGetter = idGetter;
            this.shippingCostService = shippingCostService;
        }

        /// <summary>
        ///     Displays a list of items in the current member's cart
        /// </summary>
        /// <returns>
        ///     Index view with the current member's cart items
        /// </returns>
        public async Task<ActionResult> Index()
        {
            CartViewModel model = new CartViewModel
            {
                Cart = await db.Carts.FindAsync(idGetter.GetUserId(User.Identity)),
            };

            model.ShippingCost = shippingCostService.CalculateShippingCost(
                model.Cart.TotalCartItemsPrice,
                model.Cart.Items);

            return View(model);
        }

        /// <summary>
        ///     Adds item to cart
        /// </summary>
        /// <param name="productId">the id of the product thats being added</param>
        /// <param name="isNew">bool representing if product is new or used</param>
        /// <returns>
        ///     game details  page
        /// </returns>
        public async Task<ActionResult> AddItem(Guid? productId, bool? isNew)
        {
            if (productId == null || isNew == null)
            {
                throw new HttpException(NotFound, nameof(Product));
            }

            var membersId = idGetter.GetUserId(User.Identity);
            Cart memberCart = await db.Carts.FindAsync(membersId);
            GameProduct gameProduct =
                await
                    db.GameProducts.Include(db => db.Game).
                        Include(db => db.Platform).
                        FirstOrDefaultAsync(x => x.Id == productId);

            if (gameProduct == null)
            {
                throw new HttpException(NotFound, nameof(Product));
            }
            if (memberCart == null)
            {
                throw new HttpException(NotFound, nameof(Cart));
            }
            Guid gameId = gameProduct.GameId;
            string name = gameProduct.Game.Name;
            string platform = gameProduct.Platform.PlatformName;
            CartItem cartItem = new CartItem()
            {
                MemberId = membersId,
                IsNew = isNew.Value,
                ProductId = gameProduct.Id,
                Quantity = 1
            };

            try
            {
                memberCart.Items.Add(cartItem);
                await db.SaveChangesAsync();
                this.AddAlert(
                    AlertType.Success, $"{name} for {platform} was succesfully added to your your cart.");
                Session[CART_QTY_SESSION_KEY] = memberCart.Items.Count;
            }
            catch (DbUpdateException)
            {
                this.AddAlert(
                    AlertType.Error, $"An error occured while adding {name} for {platform} to your cart");
            }

            return RedirectToAction("Details", "Games", new { id = gameId });
        }

        /// <summary>
        ///     removed selected item from the cart
        /// </summary>
        /// <param name="productId">the id of product being removed</param>
        /// <param name="isNew">represents if the product is used or new</param>
        /// <returns>
        ///     The index of the car controller
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveItem(Guid? productId, bool? isNew)
        {
            if (productId == null || isNew == null)
            {
                throw new HttpException(NotFound, nameof(Product));
            }

            Cart memberCart = await db.Carts.FindAsync(idGetter.GetUserId(User.Identity));
            if (memberCart == null)
            {
                throw new HttpException(NotFound, nameof(Cart));
            }

            CartItem cartItem =
                memberCart.Items.FirstOrDefault(x => x.ProductId == productId && x.IsNew == isNew);
            GameProduct gameProduct =
                await
                    db.GameProducts.Include(db => db.Game).
                        Include(db => db.Platform).
                        FirstOrDefaultAsync(x => x.Id == cartItem.ProductId);
            string name = gameProduct.Game.Name;
            string platform = gameProduct.Platform.PlatformName;

            try
            {
                memberCart.Items.Remove(cartItem);
                await db.SaveChangesAsync();
                this.AddAlert(
                    AlertType.Success, $"{name} for {platform} was succesfully removed for your cart");
                Session[CART_QTY_SESSION_KEY] = memberCart.Items.Count;
            }
            catch (DbUpdateException)
            {
                this.AddAlert(
                    AlertType.Error,
                    $"An error occured while removing {name} for {platform} from your cart");
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Updates the quantity of an item
        /// </summary>
        /// <param name="productId">
        ///     The Id of the product to change the quantity of
        /// </param>
        /// <param name="isNew">
        ///     True if the item is new
        /// </param>
        /// <param name="quantity">
        ///     The new quantity for the item
        /// </param>
        /// <returns>
        ///     Index view with the current member's cart items updated with the new quantity for the specified line
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateQuantity(Guid? productId, bool? isNew, int? quantity)
        {
            if (productId == null || isNew == null || quantity == null)
            {
                return RedirectToAction("Index");
            }

            if (quantity.Value == 0)
            {
                return RedirectToAction("RemoveItem", new { productId = productId, isNew = isNew });
            }

            if (quantity.Value < 0)
            {
                this.AddAlert(
                    AlertType.Error,
                    "Your cart cannot contain less than 1 of a product. Consider removing the item from your cart instead.");
                return RedirectToAction("Index");
            }

            Cart currentMemberCart = await db.Carts.FindAsync(idGetter.GetUserId(User.Identity));
            CartItem item =
                currentMemberCart.Items.FirstOrDefault(i => i.ProductId == productId && i.IsNew == isNew);

            if (item == null)
            {
                throw new HttpException(NotFound, nameof(CartItem));
            }

            if (isNew.Value)
            {
                int newInventory = item.Product.NewInventory;

                if (newInventory < quantity.Value)
                {
                    this.AddAlert(
                        AlertType.Info,
                        $"We currently have {newInventory} new copies of {item.Product.Name}. Your order may take longer to process than usual.");
                }
            }
            else
            {
                int usedInventory = item.Product.UsedInventory;

                if (usedInventory < quantity.Value)
                {
                    quantity = usedInventory;
                    this.AddAlert(
                        AlertType.Warning,
                        $"We currently have {usedInventory} used copies of {item.Product.Name}. Your cart has been set to the maximum deliverable quantity.");
                }
            }

            item.Quantity = quantity.Value;
            await db.SaveChangesAsync();

            this.AddAlert(AlertType.Success, $"{item.Product.Name} quantity set to {quantity}.");

            CartViewModel model = new CartViewModel
            {
                Cart = await db.Carts.FindAsync(idGetter.GetUserId(User.Identity)),
            };

            model.ShippingCost = shippingCostService.CalculateShippingCost(
                model.Cart.TotalCartItemsPrice,
                model.Cart.Items);

            return View("Index", model);
        }

        /// <summary>
        ///     Stores the number of items in the current member's cart in the Session.
        /// </summary>
        [ChildActionOnly]
        public void SetSessionCartQty()
        {
            Guid currentUserId = idGetter.GetUserId(User.Identity);

            int cartCount =
                db.Carts.Where(c => c.MemberId == currentUserId).
                    Select(c => c.Items.Count).
                    SingleOrDefault();

            Session[CART_QTY_SESSION_KEY] = cartCount;
        }
    }
}