using dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Security.Claims;
using web_service.Data;
using web_service.Services.Authentication;

namespace web_service
{
    public class AccountWebService : IAccountWebService
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly ApplicationUserManager _userManager;

        public AccountWebService(IRepository<Account> accountRepository, ApplicationUserManager userManager)
        {
            this._accountRepository = accountRepository;
            this._userManager = userManager;
        }

        public bool Create(Account user, string password)
        {
            var result = _userManager.Create(new IdentityUser
            {
                UserName = user.Email,
                Email = user.Email
            }, password);

            if (result.Succeeded)
            {
                _accountRepository.Create(user);
            }

            return result.Succeeded;
        }

        public ClaimsIdentity CreateIdentity(string userId)
        {
            var user = _userManager.FindById(userId);
            return _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public string GetSecurityStamp(string userId)
        {
            return _userManager.GetSecurityStamp(userId);
        }

        public bool IdentityUserExistsById(string userId)
        {
            var user = _userManager.FindById(userId);
            return user != null;
        }

        public bool SupportsUserSecurityStamp()
        {
            return _userManager.SupportsUserSecurityStamp;
        }

        public IdentityLoginResult ValidateIdentityUser(string username, string password, bool shouldLockout)
        {
            var user = _userManager.FindByName(username);
            if (user == null)
                return new IdentityLoginResult { CustomerLoginResults = CustomerLoginResults.MemberNotExists };

            if (_userManager.IsLockedOut(user.Id))
                return new IdentityLoginResult { CustomerLoginResults = CustomerLoginResults.IsLockedOut };

            var account = _accountRepository.Table.Where(x => x.Email.ToLower() == user.Email.ToLower());
            if (account == null)
                return new IdentityLoginResult { CustomerLoginResults = CustomerLoginResults.AccountNotExists };

            if (_userManager.CheckPassword(user, password))
            {
                _userManager.ResetAccessFailedCount(user.Id);

                var userIdentity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                return new IdentityLoginResult
                {
                    ClaimsIdentity = userIdentity,
                    CustomerLoginResults = CustomerLoginResults.Successful,
                };
            }

            if (shouldLockout)
            {
                _userManager.AccessFailed(user.Id);
                if (_userManager.IsLockedOut(user.Id))
                    return new IdentityLoginResult { CustomerLoginResults = CustomerLoginResults.IsLockedOut };
            }

            return new IdentityLoginResult { CustomerLoginResults = CustomerLoginResults.WrongPassword };
        }
    }
}
