using System.Web.Mvc;
using Topicos.Models;

namespace Topicos.Controllers
{
    public class BaseController : Controller
    {
        protected UsuarioLogado CurrentUser
        {
            get
            {
                return (UsuarioLogado)HttpContext.Session["Usuario"];
            }
            set
            {
                HttpContext.Session["Usuario"] = value;
            }
        }
    }
}