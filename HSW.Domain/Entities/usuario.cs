using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Domain.Entities
{
    public sealed class usuario
    {
         public int Id_usuario { get; set; }
         public string Nombre { get; set; } = string.Empty;
         public string Apellido { get; set; } = string.Empty;
         public string Email { get; set; } = string.Empty;
         public string contraseña { get; set; } = string.Empty;
         public string admins { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public int confirmado { get; set; }
        public string token { get; set; } = string.Empty;

    }
}
