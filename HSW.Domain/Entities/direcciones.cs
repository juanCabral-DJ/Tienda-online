using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Domain.Entities
{
    public sealed class direcciones
    {
        public int Id_direccion { get; set; }
        public int Id_usuario { get; set; }
        public string calle { get; set; } = string.Empty;
        public string ciudad { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
        public string codigo_postal { get; set; } = string.Empty;
        public string pais { get; set; } = string.Empty;
    }
}
