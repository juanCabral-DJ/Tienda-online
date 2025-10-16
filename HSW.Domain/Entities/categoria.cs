using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Domain.Entities
{
    public sealed class categoria
    {
        public int Id_categoria { get; set; }
        public string nombre_categoria { get; set; } = string.Empty; 
    }
}
