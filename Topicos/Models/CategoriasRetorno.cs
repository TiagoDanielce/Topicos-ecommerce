using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Topicos.Models.Enums;

namespace Topicos.Models
{
    public class CategoriasRetorno
    {
        public string Nome { get; set; }
        public int Qtde { get; set; }
        public CategoriaProduto Categoria { get; set; }
    }
}