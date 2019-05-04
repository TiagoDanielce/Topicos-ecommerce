using System.Collections.Generic;

namespace Topicos.Models
{
    public class CarrinhoModel : BaseModel
    {
        public CarrinhoModel()
        {
            Produtos = new List<CarrinhoItemModel>();
        }

        public string UsuarioId { get; set; }

        public List<CarrinhoItemModel> Produtos { get; set; }
    }
}