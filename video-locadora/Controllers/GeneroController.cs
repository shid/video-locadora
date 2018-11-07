using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using video_locadora.Models;

namespace video_locadora.Controllers
{
    public class GeneroController : _Customizado
    {
        // Variaveis privadas utilizada somente na class
        private Genero genero;

        // Método Index: Chama a página dos Generos
        public ActionResult Index()
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                return View(db.Generos.ToList()); // Caso esteja logado, redireciona para view onde lista todos os generos
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Adicionar: Chama a página para adicionar um novo Genero 
        public ActionResult Adicionar()
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                return View(); // Caso esteja logado, redireciona para view de adicionar um novo genero
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Editar: Retorna o genero por meio do ID
        public ActionResult Editar(Genero model)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                genero = db.Generos.Find(model.Id); // Localisa genero por ID

                return Json(genero, JsonRequestBehavior.AllowGet); // Caso esteja logado, retorna o resultado da consulta em json
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Apagar: Apaga 1 ou mais generos selecionados
        public ActionResult Apagar(FormCollection collection)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                string[] ids = collection["checkbox"].Split(new char[] { ',' }); // Separa todos os valores e armazena em uma array

                List<Genero> genero_lista = new List<Genero>(); // Cria uma lista com a estrutura do Model Genero

                foreach (String id in ids) // Faz a leitura de todos os valores da array ids
                {
                    Genero genero = db.Generos.Find(Int64.Parse(id)); // Localiza todos os ids da base de dados e armazena o resultado na variavel genero
                    genero_lista.Add(genero); // Adiciona todos os valores encontrado na lista genero_lista
                }

                db.Generos.RemoveRange(genero_lista); // Remove todos os generos listados na variavel genero_lista

                db.SaveChanges(); // Salva as alterações feitas no banco de dados

                Alert("ativo", "success", _SUCESSO_, _REGISTRO_DELETADO_); // Alerta informativo para retornar uma ação executada ou um aviso/erro

                return RedirectToAction("Index"); // Caso esteja logado, redireciona para o método Index do GeneroController
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }

        }

        // Método Salvar: Adiciona um novo filme
        public ActionResult Salvar(Genero model)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                if (model.Nome != null) // Verifica se não é null a variavel Nome 
                {
                    genero = new Genero // Criaçao de um novo objeto
                    {
                        // Passando dados do formulário para o objeto genero
                        Nome = (model.Nome),
                        Ativo = model.Ativo,
                        Data_Criacao = _DATETIME_NOW_
                    };

                    db.Generos.Add(genero); // Incluindo um novo genero no banco de dados, com os dados inseridos no objeto genero

                    db.SaveChanges(); // Salva as alterações feitas no banco de dados

                    Alert("ativo", "success", _SUCESSO_, _REGISTRO_SALVO_); // Alerta informativo para retornar uma ação executada ou um aviso/erro
                }
                else
                {
                    Alert("ativo", "danger", _ERRO_, _REGISTRO_FALHA_); // Alerta informativo para retornar uma ação executada ou um aviso/erro
                }
                return RedirectToAction("Adicionar"); // Caso esteja logado, redireciona para o método Adicionar do GeneroController
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Atualizar: Atualiza um genero de acordo com o ID selecionado
        public ActionResult Atualizar(FormCollection model)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                if (model["Nome"] != "") // Verifica se não é null variavel Nome
                {
                    genero = db.Generos.Find((Int64.Parse(model["Id"])));

                    // Passando dados do formulário para o objeto genero
                    genero.Nome = (model["Nome"]);
                    genero.Ativo = model.AllKeys.Contains("Ativo") ? true : false; // Verifica se existe a Key Ativo no collection

                    db.SaveChanges(); // Salva as alterações feitas no banco de dados

                    Alert("ativo", "success", _SUCESSO_, _REGISTRO_ATUALIZADO_); // Alerta informativo para retornar uma ação executada ou um aviso/erro
                }
                else
                {
                    Alert("ativo", "danger", _ERRO_, _REGISTRO_FALHA_); // Alerta informativo para retornar uma ação executada ou um aviso/erro
                }

                return RedirectToAction("Index"); // Caso esteja logado, redireciona para o método Index do GeneroController
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }
        // Metodo VerificaId: Verifica se existe o ID na tabela Genero
        public JsonResult VerificaId(FormCollection collection)
        {
            int id = int.Parse(collection["Filme.Genero_Id"]); // Converte o valor passado na View para integer
            return Json(db.Generos.Any(u => u.Id == id), JsonRequestBehavior.AllowGet); // Retorna o resultado em Json da consulta feita no banco de dados caso positivo, retorna true e negativo false
        }
    }
}