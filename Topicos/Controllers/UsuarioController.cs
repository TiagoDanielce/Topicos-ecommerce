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

        // GET
        public ActionResult Edit(string id)
        {
            ViewBag.Admin = true;
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
            ViewBag.Admin = true;
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