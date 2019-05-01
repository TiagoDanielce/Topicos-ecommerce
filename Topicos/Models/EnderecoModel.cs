using System.ComponentModel.DataAnnotations;

namespace Topicos.Models
{
    public class EnderecoModel
    {
        public string CEP { get; set; }

        public string UF { get; set; }

        public string Cidade { get; set; }

        public string Bairro { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        public string Complemento { get; set; }
    }
}