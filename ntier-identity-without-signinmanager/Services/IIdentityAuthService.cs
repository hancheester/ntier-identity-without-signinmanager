using dto;
using System.Security.Claims;

namespace ntier_identity_without_signinmanager.Services
{
    public interface IIdentityAuthService : IAuthService
    {
        bool IdentityUserExistsById(string userId);
        bool SupportsUserSecurityStamp();
        string GetSecurityStamp(string userId);
        ClaimsIdentity CreateIdentity(string userId);
        bool Create(Account account, string password);        
    }
}