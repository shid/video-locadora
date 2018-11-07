using System;
using System.Linq;
using System.Web.Mvc;
using video_locadora.Models;

namespace video_locadora.Controllers
{
    public class UsuarioController : _Customizado
    {
        // Variaveis privadas utilizada somente na class
        private Usuario usuario;

        // Método Adicionar: Chama a página dos Editar
        public ActionResult Adicionar()
        {
            return View();
        }

        // Método Editar: Retorna o usuario por meio do ID
        public ActionResult Editar(Usuario model)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                usuario = db.Usuarios.Find(Session["Credencial_Id"]); // Localisa usuario por ID
                
                return View(usuario); // Caso esteja logado, redireciona para view de edição do usuário
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Salvar: Adiciona um novo usuário
        public ActionResult Salvar(Usuario model)
        {
            if (model.Nome != null)
            {
                usuario = new Usuario // Criaçao de um novo objeto
                {
                    // Passando dados do formulário para o objeto usuario
                    Nome = (model.Nome),
                    Email = model.Email.ToLower(),
                    CPF = model.CPF,
                    Senha = Encrypt(model.Senha) // Encriptografa a senha para MD5
                };

                db.Usuarios.Add(usuario); // Incluindo um novo usuario no banco de dados, com os dados inseridos no objeto usuario

                db.SaveChanges(); // Salva as alterações feitas no banco de dados

                Session["Credencial_ID"] = usuario.Id; // Criação da sessão Usuario_ID com o valor da criação do novo usuário
                Session["Credencial_Nome"] = usuario.Nome; // Criação da sessão Usuario_Nome com o valor da criação do novo usuário

                Alert("ativo", "success", _SUCESSO_, _REGISTRO_SALVO_); // Alerta informativo para retornar uma ação executada ou um aviso/erro

                return RedirecionaPainelPrincipal(); // Redireciona para o painel principal
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Atualizar: Atualiza um usuário de acordo com o ID selecionado
        public ActionResult Atualizar(FormCollection model)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                if (model["Senha"] != "")
                {
                    usuario = db.Usuarios.Find(Session["Credencial_Id"]);

                    // Passando dados do formulário para o objeto genero
                    usuario.Senha = Encrypt(model["Senha"]);

                    db.SaveChanges(); // Salva as alterações feitas no banco de dados

                    Alert("ativo", "success", _SUCESSO_, _REGISTRO_ATUALIZADO_); // Alerta informativo para retornar uma ação executada ou um aviso/erro
                }
                else
                {
                    Alert("ativo", "danger", _ERRO_, _REGISTRO_FALHA_); // Alerta informativo para retornar uma ação executada ou um aviso/erro
                }

                return RedirecionaPainelPrincipal(); // Redireciona para o painel principal
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Metodo VerificaId: Verifica se existe o CPF na tabela Usuario
        public JsonResult VerificaCPF (string CPF)
        {
            return Json(!db.Usuarios.Any(u => u.CPF == CPF), JsonRequestBehavior.AllowGet); // Retorna o resultado em Json da consulta feita no banco de dados caso positivo, retorna true e negativo false
        }

        // Metodo VerificaId: Verifica se existe o Email na tabela Usuario
        public JsonResult VerificaEmail(string Email)
        {
            return Json(!db.Usuarios.Any(u => u.Email == Email), JsonRequestBehavior.AllowGet); // Retorna o resultado em Json da consulta feita no banco de dados caso positivo, retorna true e negativo false
        }

        // Metodo VerificaId: Verifica se existe o ID na tabela Usuario
        public JsonResult VerificaId(FormCollection collection)
        {
            int id = int.Parse(collection["Usuario.Id"]); // Converte o valor passado na View para integer
            return Json(db.Usuarios.Any(u => u.Id == id), JsonRequestBehavior.AllowGet); // Retorna o resultado em Json da consulta feita no banco de dados caso positivo, retorna true e negativo false
        }
    }
}