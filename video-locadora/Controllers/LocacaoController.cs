using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using video_locadora.Models;

namespace video_locadora.Controllers
{
    public class LocacaoController : _Customizado
    {
        public ActionResult Index()
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                var multList = new MultList();

                using (IDbConnection db = new SqlConnection(DapperDBConnection)) // Estabelecendo conexão com o banco de dados
                {
                    multList.LocacaoUsuarios = db.Query<LocacaoUsuario>("SELECT Id AS Usuario_Id, Nome AS Usuario_Nome, CPF AS Usuario_CPF, Total_Locado FROM (SELECT Usuario_Id, COUNT([Usuario_Id]) AS Total_Locado FROM [Locacao] WHERE [Devolucao] = 0 GROUP BY [Usuario_Id]) AS l LEFT JOIN [Usuario] AS u ON l.[Usuario_Id] = u.[Id];").ToList();
                    multList.FilmesGeneros = db.Query<FilmeGenero>("SELECT f.[Id], f.[Nome], f.[Genero_Id], g.[Nome] AS Genero_Nome, f.[Ativo], f.[Data_Criacao] FROM Filme AS f LEFT JOIN Genero AS g ON f.[Genero_Id] = g.[Id]").ToList();
                }
                //return Json(multList, JsonRequestBehavior.AllowGet);
                return View(multList); // Caso esteja logado, retorna o resultado da consulta em json
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Visualizar: Retorna lista de locações por usuários
        public ActionResult Lista(LocacaoFilme locacaoFilme)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                var multList = new MultList();
                using (IDbConnection db = new SqlConnection(DapperDBConnection)) // Estabelecendo conexão com o banco de dados
                {
                    multList.LocacaoFilmes = db.Query<LocacaoFilme>("SELECT l.[Id], l.[Usuario_Id] AS Usuario_Id, f.[Id] AS Filme_Id, f.[Nome] AS Filme_Nome, g.[Nome] AS Genero_Nome, l.[Data_Locacao] AS Data_Locacao FROM [Locacao] AS l LEFT JOIN [Filme] AS f ON l.[Filme_Id] = f.[Id] LEFT JOIN [Genero] AS g ON g.[Id] = f.[Genero_Id] WHERE l.[Devolucao] = 0 AND l.[Usuario_Id] = " + locacaoFilme.Id).ToList(); // Localisa genero por ID
                }

                return Json(multList.LocacaoFilmes, JsonRequestBehavior.AllowGet); // Caso esteja logado, retorna o resultado da consulta em json
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Retornar: Atualiza 1 ou mais filmes selecionados na tabela Locacao
        public ActionResult Retornar(FormCollection locacao)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {

                //return Json(locacao, JsonRequestBehavior.AllowGet);
                string[] ids = locacao["checkbox"].Split(new char[] { ',' });

                using (IDbConnection db = new SqlConnection(DapperDBConnection)) // Estabelecendo conexão com o banco de dados
                {
                    foreach (String id in ids) // Faz a leitura de todos os valores da array ids
                    {
                        db.Execute("UPDATE [Locacao] SET [Devolucao] = 1 WHERE [Id] =  " + id); // Altera o valor da coluna Devolução
                    }
                }

                return RedirectToAction("Index"); // Caso esteja logado, redireciona para o método Index do FilmeController
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }
        // Método Adicionar: Chama a página para adicionar uma nova Locação 
        public ActionResult Adicionar()
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                var multList = new MultList();

                using (IDbConnection db = new SqlConnection(DapperDBConnection)) // Estabelecendo conexão com o banco de dados
                {
                    multList.Usuarios = db.Query<Usuario>("SELECT [Id], [Nome], [CPF] FROM Usuario ORDER BY [Nome]").ToList(); // Query para listar todos os Usuários
                    multList.Filmes = db.Query<Filme>("SELECT [Id], [Nome], [Ativo] FROM Filme WHERE [Ativo] = 1 ORDER BY [Nome]").ToList(); // Query para listar todos os Filmes ativos
                }

                return View(multList); // Caso esteja logado, retorna o resultado da consulta em json
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Salvar: Adiciona uma nova Locação
        public ActionResult Salvar(FormCollection model)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                Locacao locacao = new Locacao // Criaçao de um novo objeto
                {
                    // Passando dados do formulário para o objeto locacao
                    Usuario_Id = int.Parse(model["Usuario.Id"]),
                    Filme_Id = int.Parse(model["Filme.Id"]),
                    Data_Locacao = _DATETIME_NOW_
                };
                int Ativo_Status = model.AllKeys.Contains("Ativo") ? 1 : 0; // Condição para verificar se a key Ativo existe dentro da array

                using (IDbConnection db = new SqlConnection(DapperDBConnection)) // Estabelecendo conexão com o banco de dados
                {
                    db.Execute("INSERT INTO [dbo].[Locacao] ([Usuario_Id], [Filme_Id], [Data_Locacao], [Devolucao]) VALUES ('" + locacao.Usuario_Id + "', " + locacao.Filme_Id + ", '" + locacao.Data_Locacao + "', 0)"); // Query para adicionar um novo filme
                }

                Session["Ultimo_Usuario_Locacao"] = locacao.Usuario_Id; // Grava o último usuario salvo para facilitar a inclusão dos outros filmes para um usuário selecionado

                Alert("ativo", "success", _SUCESSO_, _REGISTRO_SALVO_); // Alerta informativo para retornar uma ação executada ou um aviso/erro

                return RedirectToAction("Adicionar"); // Caso esteja logado, redireciona para o método Adicionar da LocacaoController
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }
    }
}