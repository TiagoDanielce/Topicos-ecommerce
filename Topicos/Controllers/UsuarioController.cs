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
    public class UsuarioController : BaseController
    {
        readonly ContextTopicos db = new ContextTopicos();
        // GET: Usuario
        public ActionResult Index()
        {
            ViewBag.Admin = CurrentUser == null || CurrentUser.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.User = CurrentUser == null ? "Logar" : "Bem Vindo";
            ViewBag.ExibeFooter = false;

            var list = db.UsuariosDB.Find(p => true).ToList();
            return View(list);
        }

        public ActionResult Create(UsuarioModel usuario)
        {
            ViewBag.Admin = CurrentUser == null || CurrentUser.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.User = CurrentUser == null ? "Logar" : "Bem Vindo";
            ViewBag.ExibeFooter = true;

            if (usuario != null)
            {
                usuario.Perfil = PerfilUsuario.Cliente;
                db.UsuariosDB.InsertOne(usuario);
                //return RedirectToAction("Edit","Usuario",usuario.Id);
            }
            return RedirectToAction("Index", "Login", null);
        }

        // GET
        public ActionResult Edit(string id)
        {
            ViewBag.Admin = CurrentUser == null || CurrentUser.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.User = CurrentUser == null ? "Logar" : "Bem Vindo";
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
            ViewBag.Admin = CurrentUser == null || CurrentUser.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.User = CurrentUser == null ? "Logar" : "Bem Vindo";
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

        [HttpPost]
        public ActionResult Delete(string id)
        {
            ViewBag.Admin = CurrentUser == null || CurrentUser.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.User = CurrentUser == null ? "Logar" : "Bem Vindo";
            ViewBag.ExibeFooter = false;

            if (!string.IsNullOrEmpty(id))
                db.UsuariosDB.FindOneAndDelete(p => p.Id == id);

            return RedirectToAction(nameof(Index));
        }
    }
}