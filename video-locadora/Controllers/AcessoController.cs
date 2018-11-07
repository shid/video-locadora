using System.Linq;
using System.Web.Mvc;
using video_locadora.Models;

namespace video_locadora.Controllers
{
    public class AcessoController : _Customizado
    {
        // Método Index: Chama a página principal de login
        public ActionResult Index()
        {
            return View();
        }

        // Método Login: Autenticação de acesso
        public ActionResult Login(Usuario model)
        {
            string password = Encrypt(model.Senha); // Encriptação da senha

            Usuario usuario = db.Usuarios.SingleOrDefault(u => u.CPF == model.CPF && u.Senha == password); // Consulta no banco de dados pelo CPF e Senha

            if (usuario != null) // Condição de acordo com a consulta no banco de dados
            {
                Session["Credencial_ID"] = usuario.Id; // Criação da sessão Usuario_ID com o valor consultado do banco de dados
                Session["Credencial_Nome"] = usuario.Nome; // Criação da sessão Usuario_Nome com o valor consultado do banco de dados

                Alert("ativo", "primary", usuario.Nome + ",", "Bem Vindo!"); // Alerta informativo para retornar uma ação executada ou um aviso/erro

                return RedirecionaPainelPrincipal(); // Redireciona para o painel principal
            }

            Alert("ativo", "danger", _ERRO_, _ACESSO_ERROR_); // Alerta informativo para retornar uma ação executada ou um aviso/erro

            return RedirecionaPaginaLogin(); // Caso não encontre o usuário a consulta, é redirecionado para página de login

        }

        // Método Logout: Apaga a sessão criada depois de estar logado
        public ActionResult Logout()
        {
            Session.Clear(); // Limpa todos os dados da sessão
            Session.Abandon(); // Cancela todas as sessões

            Alert("ativo", "success", _SUCESSO_, _ACESSO_ENCERRADO_); // Alerta informativo para retornar uma ação executada ou um aviso/erro

            return RedirecionaPaginaLogin(); // Redireciona para página de login
        }
    }
}