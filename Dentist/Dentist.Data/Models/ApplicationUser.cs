using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dentist.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }
        [Range(0,100)]
        public int Years { get; set; }
   
        public string StudioId { get; set; }
        [ForeignKey("StudioId")]
        public virtual DentistStudio Studio { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }


    }
}
