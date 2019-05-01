using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Topicos.Models
{
    public class VendaItemModel
    {
        public string ProdutoId { get; set; }

        public int Quantidade { get; set; }

        public decimal Preco { get; set; }
    }
}