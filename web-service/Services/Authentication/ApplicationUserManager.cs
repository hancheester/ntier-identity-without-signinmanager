using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace web_service.Services.Authentication
{
    public class ApplicationUserManager : UserManager<IdentityUser>
    {
        public ApplicationUserManager(ApplicationUserStore store)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<IdentityUser>(this)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;
        }
    }
}