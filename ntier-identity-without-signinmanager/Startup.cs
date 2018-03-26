using Autofac;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ntier_identity_without_signinmanager.Startup))]
namespace ntier_identity_without_signinmanager
{
    public partial class Startup
    {
        public static IContainer Container { get; set; }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app, Container);
        }
    }
}
