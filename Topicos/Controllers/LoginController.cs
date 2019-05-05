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
    public class LoginController : BaseController
    {
        ContextTopicos db = new ContextTopicos();

        // GET: Login
        public ActionResult Index()
        {
            ViewBag.Admin = CurrentUser == null || CurrentUser.Perfil == PerfilUsuario.Cliente ? false : true;
            ViewBag.User = CurrentUser == null ? "Logar" : "Bem Vindo";
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
        public ActionResult Login(LoginModel login)
        {
            ViewBag.Admin = true;
            ViewBag.User = CurrentUser == null ? "Logar" : "Bem Vindo";
            ViewBag.ExibeFooter = true;

            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = db.UsuariosDB.Find(p => p.Email.ToLower() == login.Usuario.ToLower() && p.Senha == login.Senha).FirstOrDefault();

                    if (usuario != null)
                    {
                        var user = new UsuarioLogado
                        {
                            Id = usuario.Id,
                            Nome = usuario.NomeCompleto,
                            Perfil = usuario.Perfil
                        };
                        Session.RemoveAll();
                        HttpContext.Session["Usuario"] = user;
                        ViewBag.Admin = user.Perfil == PerfilUsuario.Cliente ? false : true;
                        ViewBag.User = CurrentUser == null ? "Logar" : "Bem Vindo";
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

            //FormsAuthentication.SignOut();
            Session.RemoveAll();

            return View("Index");
        }
    }
}