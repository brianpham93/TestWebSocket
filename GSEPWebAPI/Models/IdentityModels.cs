using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;

namespace GSEPWebAPI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class GSEPUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<GSEPUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

    }

    public class GSEPDbContext : IdentityDbContext<GSEPUser>
    {
        //GSEP is connection string name, change in web.config
        public GSEPDbContext()
            : base("GSEP", throwIfV1Schema: false)
        {
        }

        public static GSEPDbContext Create()
        {
            return new GSEPDbContext();
        }
                
    }


    public class GSEPRole : IdentityRole
    {
        public string Description { get; set; }

        public GSEPRole()
        {

        }

        public GSEPRole(string roleName, string description) : base(roleName)
        {
            this.Description = description;
        }        
    }

}