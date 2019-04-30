using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Topicos.Models;

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
            return View();
        }

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
                        return Redirect(ReturnUrl ?? "~/Relatorio/Index");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Ocorreu um erro inesperado!";
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