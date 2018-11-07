using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace video_locadora.Models
{
    [Table("Usuario")] // Nome da Tabela do banco de dados

    // Estrutura da tabela princial de Filmes
    public partial class Usuario
    {
        public long Id { get; set; } // Cria��o da coluna Id com permiss�o de leitura e escrita

        [Required(ErrorMessage = "Complete esse campo")] // Valida��o do campo como requerido e com retorno de mensagem personalizada
        [StringLength(50)] // Valida��o do campo com valor m�ximo de caracteres
        public string Nome { get; set; } // Cria��o da coluna Nome com permiss�o de leitura e escrita

        [Required(ErrorMessage = "Complete esse campo")]
        [StringLength(50)]
        [Remote("VerificaEmail", "Usuario", HttpMethod = "POST", ErrorMessage = "Email j� cadastrado")] // Verifica��o personalizada por script com retorno de mensagem personalizado
        public string Email { get; set; }

        [Required(ErrorMessage = "Complete esse campo")]
        [StringLength(14)]
        [Remote("VerificaCPF", "Usuario", HttpMethod = "POST", ErrorMessage = "CPF j� cadastrado")] // Verifica��o personalizada por script com retorno de mensagem personalizado
        public string CPF { get; set; }

        [Required(ErrorMessage = "Complete esse campo")]
        [StringLength(50)]
        public string Senha { get; set; }
    }

    // Cria��o de uma estrutura de dados de Usuario e Login
    public partial class UsuarioLogin
    {
        [Required(ErrorMessage = "Complete esse campo")]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Complete esse campo")]
        [StringLength(50)]
        public string Senha { get; set; }
    }

    // Cria��o de uma estrutura de dados de Usuario e Loca��o
    public partial class UsuarioLocacao
    {
        [Required(ErrorMessage = "Complete esse campo")] // Valida��o do campo como requerido e com retorno de mensagem personalizada
        [Remote("VerificaId", "Usuario", HttpMethod = "POST", ErrorMessage = "Usu�rio n�o encontrado")] // Verifica��o personalizada por script com retorno de mensagem personalizado
        public long Id { get; set; }
    }

}
