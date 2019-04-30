using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Topicos.Models;
using Topicos.Models.Enums;

namespace Topicos.Controllers
{
    public class LoginController : Controller
    {
        ContextTopicos db = new ContextTopicos();

        // GET: Login
        public ActionResult Index()
        {
            ViewBag.Admin = true;
            ViewBag.ExibeFooter = true;

            if (db.UsuariosDB.Find(p=>true).Count() == 0)
            {
                var user = new UsuarioModel()
                {
                    Cpf = "03636132008",
                    Email = "tiagodanielce@gmail.com",
                    NomeCompleto = "Tiago Rodrigues Danielce",
                    Telefone = "51994611521",
                    Senha = "123456",
                    Perfil = PerfilUsuario.Admin
                };
                db.UsuariosDB.InsertOne(user);
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login, string ReturnUrl)
        {
            ViewBag.Admin = true;
            ViewBag.ExibeFooter = true;

            if (ModelState.IsValid)
            {
                try
                {
                    if (login.ValidarUsuario())
                    {
                        login.RegistrarLogin(HttpContext);

                        //verificar depois
                        return Redirect("~/Home/Index");
                    }
                }
                catch (Exception ex)
                {
                    return View("Index");
                }
            }

            return View("Index");
        }

        public ActionResult LogOff()
        {
            ViewBag.Admin = true;
            ViewBag.ExibeFooter = true;

            FormsAuthentication.SignOut();
            Session.RemoveAll();

            return View("Index");
        }
    }
}