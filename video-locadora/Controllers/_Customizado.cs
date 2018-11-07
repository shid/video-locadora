using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;
using video_locadora.Models;
using System.Configuration;
using System;

namespace video_locadora.Controllers
{
    public class _Customizado : Controller
    {
        // Variaveis publicas utilizada em todo o sistema
        public ModelPrincipal db = new ModelPrincipal(); // Criação da conexão do banco de dados
        public string DapperDBConnection = ConfigurationManager.ConnectionStrings["DB_VideoLocadora"].ConnectionString;

        // Constantes informativo de alerta
        public const string _ERRO_ = "Erro:";
        public const string _SUCESSO_ = "Sucesso:";
        public const string _AVISO_ = "Aviso:";

        public const string _REGISTRO_DELETADO_ = "Registro deletado com sucesso.";
        public const string _REGISTRO_SALVO_ = "Registro salvo com sucesso.";
        public const string _REGISTRO_ATUALIZADO_ = "Registro atualizado com sucesso.";
        public const string _REGISTRO_FALHA_ = "Não foi possivel salvar, por favor, verifique os dados informados.";

        public const string _ACESSO_ERROR_ = "Usuario ou senha não encontrado em nosso sistema.";
        public const string _ACESSO_NEGADO_ = "Você não tem permissão para acessar essa página.";
        public const string _ACESSO_SUCESSO_ = "Acesso realizado com sucesso.";
        public const string _ACESSO_ENCERRADO_ = "Sessão encerrada com sucesso.";

        public const string _CAMPO_REQUERIDO_ = "Complete esse campo";

        // Varial com horário padrão
        public DateTime _DATETIME_NOW_ = DateTime.Now;

        // Método Encrypty MD5: Script referência https://docs.microsoft.com/pt-br/dotnet/api/system.security.cryptography.md5?redirectedfrom=MSDN&view=netframework-4.7.2
        public string Encrypt(string input)
        {
            if (input != null)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                    StringBuilder sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }
                    return sBuilder.ToString();
                }
            }
            return null;
        }

        // Método para verificar se um usuário está logado
        public bool CheckLogin()
        {
            return (Session["Credencial_Nome"] == null) ? false : true;
        }

        // Método RedirecionaPaginaLogin: Redirecionamento padrão para página de login
        public ActionResult RedirecionaPaginaLogin()
        {
            if(Session["Alert-Status"] == null)
            {
                Alert("ativo", "danger", _AVISO_, _ACESSO_NEGADO_); // Somente exibe o alerta de acesso negado, se não estiver logado
            }
            return RedirectToAction("Index", "Acesso");
        }

        // Método RedirecionaPainelPrincipal: Redirecionamento padrão para página do Painel Principal
        public ActionResult RedirecionaPainelPrincipal()
        {
            return RedirectToAction("Index", "Painel");
        }

        // Método Alerta: Ativa a mensagem de alerta com o estilo desejado
        public void Alert(string status, string style, string strong, string message)
        {
            Session["Alert-Status"] = status;
            Session["Alert-Style"] = style;
            Session["Alert-Strong"] = strong;
            Session["Alert-Message"] = message;
        }
    }
}