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


            return View(list);
        }

        public ActionResult Comprar(CarrinhoModel carrinho)
        {
            var usuario = HttpContext.Session["Usuario"] as UsuarioLogado;
            ViewBag.Admin = usuario == null || usuario.Perfil == PerfilUsuario.Cliente ? false : true;

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
            }

            return View();
        }
    }
}