using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Topicos.Models;
using Topicos.Models.Enums;

namespace Topicos.Controllers
{
    public class CarrinhoController : BaseController
    {
        readonly ContextTopicos db = new ContextTopicos();
        
        // GET: Carrinho
        public ActionResult Index()
        {
            var usuario = HttpContext.Session["Usuario"] as UsuarioLogado;
            ViewBag.Admin = usuario == null || usuario.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.ExibeFooter = true;

            if (usuario == null)
                return Redirect("~/Login");
            var list = db.CarrinhoDB.Find(p => true).FirstOrDefault(); //validar para id do usuario logado
            if (list == null)
                list = new CarrinhoModel();

            ViewBag.ValorTotal = list.Produtos.Sum(p => p.PrecoUnitario * p.Quantidade);

            return View(list);
        }

        public ActionResult Comprar()
        {
            var usuario = HttpContext.Session["Usuario"] as UsuarioLogado;
            ViewBag.Admin = usuario == null || usuario.Perfil == PerfilUsuario.Cliente ? false : true;

            CarrinhoModel carrinho = null;
            if (usuario != null)
                carrinho = db.CarrinhoDB.Find(p => p.UsuarioId == usuario.Id).FirstOrDefault();

            if (carrinho != null)
            {
                decimal valorTotal = carrinho.Produtos.Sum(p => p.Quantidade * p.PrecoUnitario);
                var model = new VendaModel()
                {
                    UsuarioId = carrinho.UsuarioId,
                    DataVenda = DateTime.Now,
                    ValorTotal = valorTotal,
                    VendaItens = carrinho.Produtos
                };
                db.VendaDB.InsertOne(model);
                db.CarrinhoDB.DeleteMany(p => p.UsuarioId == usuario.Id);
                return RedirectToAction("Index", "Home", null);
            }

            return View();
        }
    }
}