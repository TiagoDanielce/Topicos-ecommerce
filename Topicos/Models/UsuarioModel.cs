using System.ComponentModel.DataAnnotations;
using Topicos.Models.Enums;

namespace Topicos.Models
{
    public class UsuarioModel : BaseModel
    {
        [Required]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        public string Cpf { get; set; }

        public string Telefone { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public PerfilUsuario Perfil { get; set; }

        //[Required]
        [Display(Name = "Endereço")]
        public EnderecoModel Endereco { get; set; }
    }
}
