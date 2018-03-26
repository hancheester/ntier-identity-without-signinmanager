using Autofac;
using Autofac.Integration.Mvc;
using ntier_identity_without_signinmanager.Services;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ntier_identity_without_signinmanager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            builder.RegisterModule(new AutofacWebTypesModule());

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<IdentityAuthService>()
                .As<IAuthService>()
                .As<IIdentityAuthService>();

            // You can uncomment here for Form Authentication, but make sure builder.RegisterType<IdentityAuthService>()... line is disabled
            //builder.RegisterType<FormAuthService>()
            //    .As<IAuthService>();                

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            Startup.Container = container;
        }
    }
}
