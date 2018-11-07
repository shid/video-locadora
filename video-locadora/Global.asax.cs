using System.Web.Optimization;
using System.Web.Routing;

namespace video_locadora
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);      // Iniciando a implementação das rotas
            BundleConfig.RegisterBundles(BundleTable.Bundles);  // Iniciando a implementação do pacotes web
        }
    }
}
