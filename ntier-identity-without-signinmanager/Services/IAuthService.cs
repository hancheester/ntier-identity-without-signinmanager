using dto;

namespace ntier_identity_without_signinmanager.Services
{
    public interface IAuthService
    {
        CustomerLoginResults SignIn(string username, string password, bool isPersistent, bool shouldLockOut);
        void SignOut();
    }
}
