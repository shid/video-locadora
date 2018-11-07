using System.Web.Mvc;

namespace video_locadora.Controllers
{
    public class PainelController : _Customizado
    {
        // Método Index: Chama a página Principal
        public ActionResult Index()
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                return View(); // Caso esteja logado, redireciona para view do painel principal
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }
    }
}