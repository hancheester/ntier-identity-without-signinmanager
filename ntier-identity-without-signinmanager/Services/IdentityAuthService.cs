using dto;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ntier_identity_without_signinmanager.AccountService;
using System.Security.Claims;
using System.Web;

namespace ntier_identity_without_signinmanager.Services
{
    public class IdentityAuthService : IAuthService, IIdentityAuthService
    {
        private readonly HttpContextBase _httpContext;

        public IdentityAuthService(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }

        public bool Create(Account user, string password)
        {
            var result = false;
            using (AccountWebServiceClient client = new AccountWebServiceClient())
            {
                result = client.Create(user, password);
            }

            return result;
        }

        public ClaimsIdentity CreateIdentity(string userId)
        {
            using (AccountWebServiceClient client = new AccountWebServiceClient())
            {
                return client.CreateIdentity(userId);
            }
        }

        public string GetSecurityStamp(string userId)
        {
            using (AccountWebServiceClient client = new AccountWebServiceClient())
            {
                return client.GetSecurityStamp(userId);
            }
        }

        public bool IdentityUserExistsById(string userId)
        {
            using (AccountWebServiceClient client = new AccountWebServiceClient())
            {
                return client.IdentityUserExistsById(userId);
            }
        }

        public bool SupportsUserSecurityStamp()
        {
            using (AccountWebServiceClient client = new AccountWebServiceClient())
            {
                return client.SupportsUserSecurityStamp();
            }
        }

        public CustomerLoginResults SignIn(string username, string password, bool isPersistent, bool shouldLockOut)
        {
            IdentityLoginResult identityLoginResult;
            using(AccountWebServiceClient client = new AccountWebServiceClient())
            {
                identityLoginResult = client.ValidateIdentityUser(username, password, shouldLockOut);
            }

            if (identityLoginResult.CustomerLoginResults == CustomerLoginResults.Successful)
            {
                var authenticationManager = _httpContext.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
                authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identityLoginResult.ClaimsIdentity);
            }

            return identityLoginResult.CustomerLoginResults;
        }

        public void SignOut()
        {
            var authenticationManager = _httpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}