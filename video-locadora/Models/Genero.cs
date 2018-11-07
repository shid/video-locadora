using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace video_locadora.Models
{
    [Table("Genero")] // Nome da Tabela do banco de dados

    // Estrutura da tabela princial de Filmes
    public partial class Genero
    {
        public long Id { get; set; } // Criação da coluna Id com permissão de leitura e escrita

        [Required(ErrorMessage = "Complete esse campo")] // Validação do campo como requerido e com retorno de mensagem personalizada
        [StringLength(50)] // Validação do campo com valor máximo de caracteres
        public string Nome { get; set; } // Criação da coluna Nome com permissão de leitura e escrita

        public DateTime Data_Criacao { get; set; }

        public bool Ativo { get; set; }
    }
}
