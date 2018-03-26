using System.Security.Claims;

namespace dto
{
    public class IdentityLoginResult
    {
        public CustomerLoginResults CustomerLoginResults { get; set; }
        public ClaimsIdentity ClaimsIdentity { get; set; }
    }
}
