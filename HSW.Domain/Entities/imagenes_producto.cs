using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Domain.Entities
{
    public sealed class imagenes_producto
    {
        public int Id_imagen { get; set; }
        public int Id_producto { get; set; }
        public string url_imagen { get; set; } = string.Empty;
        public string texto_alternativo { get; set; } = string.Empty;
    }
}
