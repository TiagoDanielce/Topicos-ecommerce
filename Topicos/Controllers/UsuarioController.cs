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
    public class UsuarioController : Controller
    {
        readonly ContextTopicos db = new ContextTopicos();
        // GET: Usuario
        public ActionResult Index()
        {
            var usuario = HttpContext.Session["Usuario"] as UsuarioLogado;
            ViewBag.Admin = usuario == null || usuario.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.ExibeFooter = false;

            var list = db.UsuariosDB.Find(p => true).ToList();
            return View(list);
        }

        // GET
        public ActionResult Edit(string id)
        {
            var user = HttpContext.Session["Usuario"] as UsuarioLogado;
            ViewBag.Admin = user == null || user.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.ExibeFooter = false;

            var usuario = db.UsuariosDB.Find(p => p.Id == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(id))
            {
                return View(usuario);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(string id, UsuarioModel model)
        {
            var usuario = HttpContext.Session["Usuario"] as UsuarioLogado;
            ViewBag.Admin = usuario == null || usuario.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.ExibeFooter = false;

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    model.Id = id;
                    var filter = Builders<UsuarioModel>.Filter.Eq(p => p.Id, id);
                    db.UsuariosDB.ReplaceOne(filter, model);
                }
                else
                {
                    db.UsuariosDB.InsertOne(model);
                }
            }
            return View(model);
        }
    }
}