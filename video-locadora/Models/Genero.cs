using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace video_locadora.Models
{
    [Table("Genero")] // Nome da Tabela do banco de dados

    // Estrutura da tabela princial de Filmes
    public partial class Genero
    {
        public long Id { get; set; } // Cria��o da coluna Id com permiss�o de leitura e escrita

        [Required(ErrorMessage = "Complete esse campo")] // Valida��o do campo como requerido e com retorno de mensagem personalizada
        [StringLength(50)] // Valida��o do campo com valor m�ximo de caracteres
        public string Nome { get; set; } // Cria��o da coluna Nome com permiss�o de leitura e escrita

        public DateTime Data_Criacao { get; set; }

        public bool Ativo { get; set; }
    }
}
