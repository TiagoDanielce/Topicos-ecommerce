using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Topicos.Models;

namespace Topicos.Controllers
{
    public class UsuarioController : Controller
    {
        readonly ContextTopicos db = new ContextTopicos();
        // GET: Usuario
        public ActionResult Index()
        {
            ViewBag.Admin = true;
            ViewBag.ExibeFooter = false;

            var list = db.UsuariosDB.Find(p => true).ToList();
            return View(list);
        }
    }
}