using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebTemplate.Database;

namespace WebTemplate.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IoCConfig.Register();
            AutoMapperConfig.Initialize();

            //System.Data.Entity.Database.SetInitializer(new WebTemplateContextInitializer());
        }
    }
}
