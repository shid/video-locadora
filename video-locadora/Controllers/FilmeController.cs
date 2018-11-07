using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using video_locadora.Models;
using Dapper;
using System;

namespace video_locadora.Controllers
{
    public class FilmeController : _Customizado
    {
        // Variaveis privadas utilizada somente na class
        private FilmeGenero filmeGenero;
        private Filme filme;

        public ActionResult Index()
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                var multList = new MultList(); // Criação de um novo objeto com multiplas listas

                using (IDbConnection db = new SqlConnection(DapperDBConnection)) // Estabelecendo conexão com o banco de dados
                {
                    multList.FilmesGeneros = db.Query<FilmeGenero>("SELECT f.[Id], f.[Nome], f.[Genero_Id], g.[Nome] AS Genero_Nome, f.[Ativo], CONVERT(VARCHAR(10), f.[Data_Criacao], 103) FROM Filme AS f LEFT JOIN Genero AS g ON f.[Genero_Id] = g.[Id]").ToList(); // Query para retornar a lista de filmes com os nomes dos generos
                    multList.Generos = db.Query<Genero>("SELECT [Id], [Nome], [Ativo] FROM Genero WHERE [Ativo] = 1 ORDER BY [Nome]").ToList(); // Query que retorna todos os generos ativos
                }

                return View(multList); // Caso esteja logado, retorna o resultado da consulta em json
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Editar: Retorna o filme por meio do ID
        public ActionResult Editar(FilmeGenero filme)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                using (IDbConnection db = new SqlConnection(DapperDBConnection)) // Estabelecendo conexão com o banco de dados
                {
                    filmeGenero = db.Query<FilmeGenero>("SELECT f.[Id], f.[Nome], f.[Genero_Id], g.[Nome] AS Genero_Nome, f.[Ativo], f.[Data_Criacao] FROM Filme AS f LEFT JOIN Genero AS g ON f.[Genero_Id] = g.[Id] WHERE f.[Id] = " + filme.Id).SingleOrDefault(); // Localisa filme por ID com o nome/id do seu respectivo genero
                }

                return Json(filmeGenero, JsonRequestBehavior.AllowGet); // Caso esteja logado, retorna o resultado da consulta em json
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Atualizar: Atualiza um filme de acordo com o ID selecionado
        public ActionResult Atualizar(FormCollection model)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                if (model["Nome"] != "") // Verifica se não é null o nome do filme
                {
                    Filme filme = new Filme // Cria uma variavel com a estrutura Filme
                    {
                        // Passa os valores do model para a estrutura Filme
                        Id = int.Parse(model["Id"]), // Converte o valor de string para numero
                        Nome = model["Nome"],
                        Genero_Id = int.Parse(model["Genero_Id"]),
                        Ativo = model.AllKeys.Contains("Ativo") ? true : false // Verifica se existe a key Ativo na array model
                    };

                using (IDbConnection db = new SqlConnection(DapperDBConnection)) // Estabelecendo conexão com o banco de dados
                {
                    db.Execute("UPDATE Filme SET Nome = '" + filme.Nome + "', Genero_Id = " + filme.Genero_Id + ", Ativo = " + Convert.ToInt32(filme.Ativo) + " WHERE Id = " + filme.Id); // Query para atualizar o nome ou status do filme de acordo com o ID selecionado
                }

                Alert("ativo", "success", _SUCESSO_, _REGISTRO_SALVO_); // Alerta informativo para retornar uma ação executada ou um aviso/erro

                }
                else
                {
                    Alert("ativo", "danger", _ERRO_, _REGISTRO_FALHA_); // Alerta informativo para retornar uma ação executada ou um aviso/erro
                }

                return RedirectToAction("Index"); // Caso esteja logado, redireciona para o método Index do FilmeController
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Apagar: Apaga 1 ou mais filmes selecionados
        public ActionResult Apagar(FormCollection filmes)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                using (IDbConnection db = new SqlConnection(DapperDBConnection)) // Estabelecendo conexão com o banco de dados
                {
                    db.Execute("DELETE FROM Filme WHERE[Id] IN (" + filmes["checkbox"] + ")"); // Remove todos os filmes selecionados
                }

                Alert("ativo", "success", _SUCESSO_, _REGISTRO_DELETADO_); // Alerta informativo para retornar uma ação executada ou um aviso/erro

                return RedirectToAction("Index"); // Caso esteja logado, redireciona para o método Index do FilmeController
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }

        }

        // Método Adicionar: Chama a página para adicionar um novo Filme 
        public ActionResult Adicionar()
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                var multList = new MultList();

                using (IDbConnection db = new SqlConnection(DapperDBConnection)) // Estabelecendo conexão com o banco de dados
                {
                    multList.Generos = db.Query<Genero>("SELECT [Id], [Nome], [Ativo] FROM Genero WHERE [Ativo] = 1 ORDER BY [Nome]").ToList(); // Retorna todos os generos e o resultado é convertido em uma lista
                }

                return View(multList); // Caso esteja logado, retorna o resultado da consulta em json
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }

        // Método Salvar: Adiciona um novo filme
        public ActionResult Salvar(FormCollection model)
        {
            if (CheckLogin()) // Verifica se a sessão Usuario_Nome existe
            {
                if (model["Nome"] != "") // Verifica se não é null o nome do filme
                {
                    filme = new Filme // Criaçao de um novo objeto
                    {
                        // Passando dados do formulário para o objeto filme
                        Nome = (model["Filme.Nome"]),
                        Genero_Id = int.Parse(model["Filme.Genero_Id"]),
                        Ativo = model.AllKeys.Contains("Ativo") ? true : false,
                        Data_Criacao = _DATETIME_NOW_
                    };
                    using (IDbConnection db = new SqlConnection(DapperDBConnection)) // Estabelecendo conexão com o banco de dados
                    {
                        db.Execute("INSERT INTO [dbo].[Filme] ([Nome], [Genero_Id], [Ativo], [Data_Criacao]) VALUES ('" + filme.Nome + "', " + filme.Genero_Id + ", " + Convert.ToInt32(filme.Ativo) + ", '" + filme.Data_Criacao + "')"); // Query para adicionar um novo filme com base nos dados colocado no formulário
                    }
                    Alert("ativo", "success", _SUCESSO_, _REGISTRO_SALVO_); // Alerta informativo para retornar uma ação executada ou um aviso/erro
                }
                else
                {
                    Alert("ativo", "danger", _ERRO_, _REGISTRO_FALHA_); // Alerta informativo para retornar uma ação executada ou um aviso/erro
                }
                return RedirectToAction("Adicionar"); // Caso esteja logado, redireciona para o método Adicionar do FilmeController
            }
            else
            {
                return RedirecionaPaginaLogin(); // Caso contrário, redireciona para página de login
            }
        }
        // Metodo VerificaId: Verifica se existe o ID na tabela Filmes
        public JsonResult VerificaId(FormCollection collection)
        {
            int id = int.Parse(collection["Filme.Id"]); // Converte o valor passado na View para integer
            return Json(db.Filmes.Any(u => u.Id == id), JsonRequestBehavior.AllowGet); // Retorna o resultado em Json da consulta feita no banco de dados caso positivo, retorna true e negativo false
        }
    }
}