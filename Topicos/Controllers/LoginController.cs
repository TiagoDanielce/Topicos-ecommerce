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
            return View();
        }

        public ActionResult Login(LoginModel login, string ReturnUrl)
        {
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
            FormsAuthentication.SignOut();
            Session.RemoveAll();

            return View("Index");
        }
    }
}