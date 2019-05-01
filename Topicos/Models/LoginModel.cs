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
    }
}