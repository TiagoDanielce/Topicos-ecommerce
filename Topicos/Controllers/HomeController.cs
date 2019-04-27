using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Topicos.Models;
using Topicos.Models.Enums;

namespace Topicos.Controllers
{
    public class HomeController : Controller
    {
        ContextTopicos db = new ContextTopicos();

        public ActionResult Index(string filter)
        {
            ViewBag.Admin = true;
            ViewBag.ExibeFooter = true;

            var list = db.ProdutosDB.Find(p => true).ToList();

            List<CategoriasRetorno> categorias = new List<CategoriasRetorno>();
            var cat = Enum.GetValues(typeof(CategoriaProduto)).Cast<CategoriaProduto>();

            foreach (var item in cat)
            {
                var c = list.Where(p => p.Categoria == item).Count();
                categorias.Add(new CategoriasRetorno { Nome = item.ToString().ToUpper(), Qtde = c });
            }

            ViewBag.Categorias = categorias;
            if (!string.IsNullOrEmpty(filter))
                list = list.Where(p => p.Titulo.ToLower().Contains(filter.ToLower())).ToList();

            return View(list);
        }

    }
}