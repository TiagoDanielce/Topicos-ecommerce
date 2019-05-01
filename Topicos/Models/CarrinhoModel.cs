using System.Collections.Generic;

namespace Topicos.Models
{
    public class CarrinhoModel : BaseModel
    {
        public CarrinhoModel()
        {
            Produtos = new List<CarrinhoProdutoModel>();
        }

        public string UsuarioId { get; set; }

        public List<CarrinhoProdutoModel> Produtos { get; set; }
    }
}