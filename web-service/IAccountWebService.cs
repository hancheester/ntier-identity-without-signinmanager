using dto;
using System.Security.Claims;
using System.ServiceModel;

namespace web_service
{
    [ServiceContract]
    public interface IAccountWebService
    {
        [OperationContract]
        bool Create(Account user, string password);

        [OperationContract]
        IdentityLoginResult ValidateIdentityUser(string username, string password, bool shouldLockout);

        [OperationContract]
        ClaimsIdentity CreateIdentity(string userId);

        [OperationContract]
        string GetSecurityStamp(string userId);

        [OperationContract]
        bool IdentityUserExistsById(string userId);

        [OperationContract]
        bool SupportsUserSecurityStamp();
    }
}
