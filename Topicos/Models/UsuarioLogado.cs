using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Topicos.Models.Enums;

namespace Topicos.Models
{
    public class UsuarioLogado : BaseModel
    {
        public string Nome { get; set; }
        public PerfilUsuario Perfil { get; set; }
    }
}