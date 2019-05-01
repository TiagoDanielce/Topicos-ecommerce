using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Topicos.Models
{
    public class VendaModel : BaseModel
    {
        public VendaModel()
        {
            VendaItens = new List<VendaItemModel>();
        }

        public string UsuarioId { get; set; }

        [Display(Name ="Data Venda") ]
        public DateTime DataVenda { get; set; }

        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        public List<VendaItemModel> VendaItens { get; set; }
    }
}