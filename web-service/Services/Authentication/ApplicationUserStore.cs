using Microsoft.AspNet.Identity.EntityFramework;
using web_service.Data;

namespace web_service.Services.Authentication
{
    public class ApplicationUserStore : UserStore<IdentityUser>
    {
        public ApplicationUserStore(AppDbContext appDbContext)
            : base(appDbContext)
        {

        }
    }
}