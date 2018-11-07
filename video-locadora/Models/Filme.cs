using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace video_locadora.Models
{
    [Table("Filme")] // Nome da Tabela do banco de dados

    // Estrutura da tabela princial de Filmes
    public class Filme
    {
        public long Id { get; set; } // Criação da coluna Id com permissão de leitura e escrita

        [Required(ErrorMessage = "Complete esse campo")] // Validação do campo como requerido e com retorno de mensagem personalizada
        [StringLength(50)] // Validação do campo com valor máximo de caracteres
        public string Nome { get; set; } // Criação da coluna Nome com permissão de leitura e escrita

        [Required(ErrorMessage = "Complete esse campo")] // Validação do campo como requerido e com retorno de mensagem personalizada
        public long Genero_Id { get; set; }

        public DateTime Data_Criacao { get; set; }

        public bool Ativo { get; set; }

    }

    // Criação de uma estrutura de dados de Filmes e Genero
    public class FilmeGenero
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Genero_Nome { get; set; }

        public long Genero_Id { get; set; }

        public bool Ativo { get; set; }
    }

    // Criação de uma estrutura de dados de Filmes e Locação
    public partial class FilmeLocacao
    {
        [Required(ErrorMessage = "Complete esse campo")] // Validação do campo como requerido e com retorno de mensagem personalizada
        [Remote("VerificaId", "Filme", HttpMethod = "POST", ErrorMessage = "Filme não encontrado")] // Verificação personalizada por script com retorno de mensagem personalizado
        public long Id { get; set; }

        [Required(ErrorMessage = "Complete esse campo")] // Validação do campo como requerido e com retorno de mensagem personalizada
        public string Nome { get; set; }

        [Required(ErrorMessage = "Complete esse campo")] // Validação do campo como requerido e com retorno de mensagem personalizada
        [Remote("VerificaId", "Genero", HttpMethod = "POST", ErrorMessage = "Genero não encontrado")] // Verificação personalizada por script com retorno de mensagem personalizado
        public long Genero_Id { get; set; }
    }
}