using System.Web.Mvc;
using System.Web.Routing;

namespace video_locadora
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Criação da rota padrão de navegação
            routes.MapRoute(
                name: "Padrao", // Nome da rota
                url: "{controller}/{action}/{id}", // Estrutura da navegação
                defaults: new { controller = "Acesso", action = "Index", id = UrlParameter.Optional } // Rota padrão de chamada da tela de login
            );

        }

    }
}
