using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Domain.Entities
{
    public sealed class inventario
    {
        public int Id_inventario { get; set; }
        public int Id_producto { get; set; }
        public string talla { get; set; } = string.Empty;
        public int stock { get; set; }
         
    }
}
