using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace video_locadora.Models
{
    [Table("Usuario")] // Nome da Tabela do banco de dados

    // Estrutura da tabela princial de Filmes
    public partial class Usuario
    {
        public long Id { get; set; } // Criação da coluna Id com permissão de leitura e escrita

        [Required(ErrorMessage = "Complete esse campo")] // Validação do campo como requerido e com retorno de mensagem personalizada
        [StringLength(50)] // Validação do campo com valor máximo de caracteres
        public string Nome { get; set; } // Criação da coluna Nome com permissão de leitura e escrita

        [Required(ErrorMessage = "Complete esse campo")]
        [StringLength(50)]
        [Remote("VerificaEmail", "Usuario", HttpMethod = "POST", ErrorMessage = "Email já cadastrado")] // Verificação personalizada por script com retorno de mensagem personalizado
        public string Email { get; set; }

        [Required(ErrorMessage = "Complete esse campo")]
        [StringLength(14)]
        [Remote("VerificaCPF", "Usuario", HttpMethod = "POST", ErrorMessage = "CPF já cadastrado")] // Verificação personalizada por script com retorno de mensagem personalizado
        public string CPF { get; set; }

        [Required(ErrorMessage = "Complete esse campo")]
        [StringLength(50)]
        public string Senha { get; set; }
    }

    // Criação de uma estrutura de dados de Usuario e Login
    public partial class UsuarioLogin
    {
        [Required(ErrorMessage = "Complete esse campo")]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Complete esse campo")]
        [StringLength(50)]
        public string Senha { get; set; }
    }

    // Criação de uma estrutura de dados de Usuario e Locação
    public partial class UsuarioLocacao
    {
        [Required(ErrorMessage = "Complete esse campo")] // Validação do campo como requerido e com retorno de mensagem personalizada
        [Remote("VerificaId", "Usuario", HttpMethod = "POST", ErrorMessage = "Usuário não encontrado")] // Verificação personalizada por script com retorno de mensagem personalizado
        public long Id { get; set; }
    }

}
