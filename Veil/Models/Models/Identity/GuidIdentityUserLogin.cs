﻿/* GuidIdentityUserLogin.cs
 * Purpose: A custom IdentityUserLogin with Guid as the key type
 * 
 * Revision History:
 *      Drew Matheson, 2015.10.08: Created
 */

using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Veil.DataModels.Models.Identity
{
    /// <summary>
    /// IdentityUserLogin with Guid as the key type
    /// <remarks>
    ///     This class is required by Entity Framework
    /// </remarks>
    /// </summary>
    public class GuidIdentityUserLogin : IdentityUserLogin<Guid> { }
}
