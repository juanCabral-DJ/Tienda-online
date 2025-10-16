using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Domain.Entities
{
    public sealed class pedido
    {
        public int Id_pedido { get; set; }
        public int Id_usuario { get; set; }
        public DateTime fecha_pedido { get; set; } = DateTime.Now; 
        public string estado_pedido { get; set; } = string.Empty;
        public decimal total_pedido { get; set; }
        public int Id_direccion { get; set; }
    }
}
