using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Topicos.Models;
using Topicos.Models.Enums;

namespace Topicos.Controllers
{
    public class HomeController : BaseController
    {
        ContextTopicos db = new ContextTopicos();

        public ActionResult Index(string filter, CategoriaProduto? categoria)
        {
            ViewBag.Admin = CurrentUser == null || CurrentUser.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.ExibeFooter = true;

            ViewBag.User = CurrentUser == null ? "Logar" : "Bem Vindo";

            db.ProdutosDB.FindOneAndDelete(p => p.Titulo == null);
            var list = db.ProdutosDB.Find(p => true).ToList();

            List<CategoriasRetorno> categorias = new List<CategoriasRetorno>();
            var cat = Enum.GetValues(typeof(CategoriaProduto)).Cast<CategoriaProduto>();

            foreach (var item in cat)
            {
                var c = list.Where(p => p.Categoria == item).Count();
                categorias.Add(new CategoriasRetorno { Nome = item.ToString().ToUpper(), Qtde = c , Categoria = item});
            }

            ViewBag.Categorias = categorias;
            if (!string.IsNullOrEmpty(filter))
                list = list.Where(p => p.Titulo.ToLower().Contains(filter.ToLower())).ToList();

            if (categoria != null)
                list = list.Where(p => p.Categoria == categoria).ToList();


            if (CurrentUser != null)
            {
                var vendai = db.VendaDB.Find(p => p.UsuarioId == CurrentUser.Id.ToString()).ToList();

                ViewBag.Vendas = vendai.SelectMany(p => p.VendaItens.Select(x => x.ProdutoId).ToList()).ToList();

                int totalvendas = vendai.SelectMany(p => p.VendaItens.Select(x => x.ProdutoId).ToList()).ToList().Count();
                
                String[,] maisvendido = new String[totalvendas, 2];

                int contador = 0;

                String[] produtos = new String[totalvendas];
                    

                foreach (var item in ViewBag.Vendas)
                {
                    produtos[contador] = item;
                    contador++;
                }

                contador = 0;
                //Teste para verificar o total de venda de cada produto
                for (int i = 0; i < totalvendas; i++)
                {
                    for (int j = 0; j < totalvendas; j++)
                    {
                        if (produtos[i] == produtos[j])
                        {
                            contador++;
                        }
                    }
                    maisvendido[i, 0] = produtos[i];
                    maisvendido[i, 1] = contador.ToString();
                    contador = 0;
                }

                int cont = int.Parse(maisvendido[0,1]);

                ViewBag.Vendas = maisvendido[0, 0];
                //Teste para ver o produto mais vendido
                for (int i = 0; i < maisvendido.Length/2; i++)
                {
                    if (int.Parse(maisvendido[i, 1]) > cont)
                    {
                        ViewBag.Vendas = maisvendido[i,0];
                    }
                }

            }

            return View(list);
        }

    }
}