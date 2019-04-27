using System.ComponentModel.DataAnnotations;
using Topicos.Models.Enums;

namespace Topicos.Models
{
    public class ProdutoModel : BaseModel
    {
        [Required]
        [Display(Name = "Nome Produto")]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Required]
        public CategoriaProduto Categoria { get; set; }
    }
}
