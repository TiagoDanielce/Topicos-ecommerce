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
            ViewBag.Admin = CurrentUser == null || CurrentUser.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.User = CurrentUser == null ? "Logar" : "Bem Vindo";
            ViewBag.ExibeFooter = true;

            if (CurrentUser == null)
                return Redirect("~/Login");
            var list = db.CarrinhoDB.Find(p => p.UsuarioId == CurrentUser.Id).FirstOrDefault(); 
            if (list == null)
                list = new CarrinhoModel();

            ViewBag.ValorTotal = list.Produtos.Sum(p => p.PrecoUnitario * p.Quantidade);

            return View(list);
        }

        public ActionResult Comprar()
        {
            ViewBag.Admin = CurrentUser == null || CurrentUser.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.User = CurrentUser == null ? "Logar" : "Bem Vindo";
            ViewBag.ExibeFooter = true;

            CarrinhoModel carrinho = null;
            if (CurrentUser != null)
                carrinho = db.CarrinhoDB.Find(p => p.UsuarioId == CurrentUser.Id).FirstOrDefault();

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
                db.CarrinhoDB.DeleteMany(p => p.UsuarioId == CurrentUser.Id);
                return RedirectToAction("Index", "Home", null);
            }

            return View();
        }

        public ActionResult AddCarrinho(string id, int quantidade = 1)
        {
            ViewBag.Admin = CurrentUser == null || CurrentUser.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.User = CurrentUser == null ? "Logar" : "Bem Vindo";
            ViewBag.ExibeFooter = true;

            if (CurrentUser == null)
                return RedirectToAction("Index", "Login", null);
            else
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var produto = db.ProdutosDB.Find(p => p.Id == id).FirstOrDefault();
                    var item = new CarrinhoItemModel()
                    {
                        PrecoUnitario = produto.Preco,
                        ProdutoId = produto.Id,
                        Titulo = produto.Titulo,
                        Quantidade = quantidade
                    };

                    var carrinho = db.CarrinhoDB.Find(p => p.UsuarioId == CurrentUser.Id).FirstOrDefault();
                    if (carrinho == null)
                    {
                        carrinho = new CarrinhoModel()
                        {
                            UsuarioId = CurrentUser.Id,
                            Produtos = new List<CarrinhoItemModel>()
                        };
                        carrinho.Produtos.Add(item);
                        db.CarrinhoDB.InsertOne(carrinho);
                    }
                    else
                    {
                        carrinho.Produtos.Add(item);
                        var filter = Builders<CarrinhoModel>.Filter.Eq(p => p.Id, carrinho.Id);
                        db.CarrinhoDB.ReplaceOne(filter, carrinho);
                    }
                    return RedirectToAction("Index", "Carrinho", null);
                }
            }
            return View();
        }
    }
}