/* Platform.cs
 * Purpose: A class for game platforms
 * 
 * Revision History:
 *      Drew Matheson, 2015.10.03: Created
 */

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Veil.DataModels.Validation;

namespace Veil.DataModels.Models
{
    /// <summary>
    ///     A gaming platform
    /// </summary>
    public class Platform
    {
        /// <summary>
        ///     The code for the Platform
        /// </summary>
        [Key]
        [StringLength(maximumLength: 5, ErrorMessageResourceName = nameof(ErrorMessages.StringLength), ErrorMessageResourceType = typeof(ErrorMessages))]
        [Required]
        public string PlatformCode { get; set; }

        /// <summary>
        ///     The Platform's full name
        /// </summary>
        [Required]
        [StringLength(maximumLength: 128, ErrorMessageResourceName = nameof(ErrorMessages.StringLength), ErrorMessageResourceType = typeof(ErrorMessages))]
        [DisplayName("Platform")]
        public string PlatformName { get; set; }

        /// <summary>
        ///     Collection navigation property for the Members who have this platform as one of their favorites
        /// </summary>
        public virtual ICollection<Member> MembersFavoritePlatform { get; set; }

        /// <summary>
        ///     Collection navigation property for the GameProduct's on this platform
        /// </summary>
        public virtual ICollection<GameProduct> GameProducts { get; set; }
    }
}