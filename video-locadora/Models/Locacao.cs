using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace video_locadora.Models
{
    [Table("Locacao")] // Nome da Tabela do banco de dados

    // Estrutura da tabela princial de Filmes
    public class Locacao
    {
        public long Id { get; set; } // Criação da coluna Id com permissão de leitura e escrita

        [Required(ErrorMessage = "Complete esse campo")] // Validação do campo como requerido e com retorno de mensagem personalizada
        public long Usuario_Id { get; set; } // Criação da coluna Nome com permissão de leitura e escrita

        [Required(ErrorMessage = "Complete esse campo")]
        public long Filme_Id { get; set; }

        public bool Devolucao { get; set; }

        public DateTime Data_Locacao { get; set; }

    }
    // Criação de uma estrutura de dados de Locacão de Usuario
    public class LocacaoUsuario
    {
        public long Id { get; set; }

        public long Usuario_Id { get; set; }

        public string Usuario_Nome { get; set; }

        public string Usuario_CPF { get; set; }

        public int Total_Locado { get; set; }

    }
    // Criação de uma estrutura de dados de Locacão de Filmes
    public class LocacaoFilme
    {
        public long Id { get; set; }

        public long Usuario_Id { get; }

        public long Filme_Id { get; set; }

        public string Filme_Nome { get; set; }

        public string Genero_Nome { get; set; }

        public string Data_Locacao { get; set; }

    }
}