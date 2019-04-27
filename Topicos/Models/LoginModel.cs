using MongoDB.Driver;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using Topicos.Models.Enums;

namespace Topicos.Models
{
    public class LoginModel
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Id { get; set; }
        public PerfilUsuario Perfil { get; set; }

        public bool ValidarUsuario()
        {
            ContextTopicos db = new ContextTopicos();
            var usuario = db.UsuariosDB.Find(p => p.Email.ToLower().Equals(this.Usuario) && p.Senha.Equals(this.Senha)).FirstOrDefault();

            if (usuario != null)
            {

                Id = usuario.Id;
                Nome = usuario.NomeCompleto;
                Usuario = usuario.Email;
                Perfil = usuario.Perfil;

                return true;
            }
            return false;
        }

        public void RegistrarLogin(HttpContextBase context)
        {
            context.Session["usuario"] = this;

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                     1,
                     this.Usuario,
                     DateTime.Now,
                     DateTime.Now.AddHours(1),
                     false,
                     "admin"
                    );

            string formsCookieStr = FormsAuthentication.Encrypt(ticket);
            HttpCookie FormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, formsCookieStr);
            context.Response.Cookies.Add(FormsCookie);

            /*HttpCookie user = new HttpCookie("user", this.NewId.ToString());
            user.Expires = DateTime.Today.AddDays(7);
            context.Response.Cookies.Add(user);*/
        }
    }
}