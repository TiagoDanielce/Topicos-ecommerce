

using System.ComponentModel.DataAnnotations;

namespace Topicos.Models
{
    public class CarrinhoProdutoModel : BaseModel
    {
        public int Quantidade { get; set; }

        [Display(Name = "Preço Unitário")]
        public decimal PrecoUnitario { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }
    }
}