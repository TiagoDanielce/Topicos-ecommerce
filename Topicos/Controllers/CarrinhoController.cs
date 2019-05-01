using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Topicos.Models;

namespace Topicos.Controllers
{
    [Authorize]
    public class CarrinhoController : BaseController
    {
        readonly ContextTopicos db = new ContextTopicos();
        // GET: Carrinho
        public ActionResult Index()
        {
            var list = db.CarrinhoDB.Find(p => true).ToList(); //validar para id do usuario logado

            return View(list);
        }
    }
}