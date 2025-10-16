using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Domain.Entities
{
    public class detalle_pedido
    {
        public int Id_detalle { get; set; }
        public int Id_pedido { get; set; }
        public int Id_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; } 
    }
}
