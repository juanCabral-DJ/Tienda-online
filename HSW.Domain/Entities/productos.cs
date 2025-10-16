using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Domain.Entities
{
    public sealed class productos
    {
        public int Id_producto { get; set; }
        public string nombre_producto { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public decimal precio_base { get; set; }
        public int Id_categoria { get; set; }
        public string marca { get; set; } = string.Empty;
        public DateTime fecha_creacion { get; set; } = DateTime.Now; 
    }
}
