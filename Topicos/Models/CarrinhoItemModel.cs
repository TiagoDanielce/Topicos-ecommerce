

using System.ComponentModel.DataAnnotations;

namespace Topicos.Models
{
    public class CarrinhoItemModel : BaseModel
    {
        public int Quantidade { get; set; }

        public string ProdutoId { get; set; }

        [Display(Name = "Preço Unitário")]
        public decimal PrecoUnitario { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }
    }
}