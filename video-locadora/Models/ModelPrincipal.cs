using System.Collections.Generic;
using System.Data.Entity;

namespace video_locadora.Models
{
    public partial class ModelPrincipal : DbContext
    {
        public ModelPrincipal() : base("name=DB_VideoLocadora") {} // Conexão com o banco de dados

        public DbSet<Genero> Generos { get; set; } // Criação da variavel Generos com vase na estrutura da tabela Genero do banco de dados com permissão de leitura/escrita
        public DbSet<Usuario> Usuarios { get; set; } // Criação da variavel Usuaris com vase na estrutura da tabela Genero do banco de dados com permissão de leitura/escrita
        public DbSet<Filme> Filmes { get; set; } // Criação da variavel Filmes com vase na estrutura da tabela Genero do banco de dados com permissão de leitura/escrita
        public DbSet<Locacao> Locacoes { get; set; } // Criação da variavel Locacao com vase na estrutura da tabela Genero do banco de dados com permissão de leitura/escrita

    }

    //Metodo MultList: Lista 
    public class MultList
    {
        public List<Genero> Generos { get; set; } // Lista de todos os generos
        public List<FilmeGenero> FilmesGeneros { get; set; } // Listas generos associados ao Filme

        public List<LocacaoUsuario> LocacaoUsuarios { get; set; } // Lista de usuarios associados a locação
        public List<Usuario> Usuarios { get; set; } // Lista de todos os usuários
        public UsuarioLocacao Usuario { get; set; } // Lista criada para validação dos campos

        public List<LocacaoFilme> LocacaoFilmes { get; set; } // Lista de filmes associados a locação
        public List<Filme> Filmes { get; set; } // Lista de todos os filmes
        public FilmeLocacao Filme { get; set; } // Lista criada para validação dos campos
    }
}
