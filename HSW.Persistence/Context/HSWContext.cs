using HSW.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HSW.Persistence.Context
{
    public class HSWContext : DbContext
    {
        public HSWContext(DbContextOptions<HSWContext> options) : base(options)
        {
        }

        public DbSet<usuario> usuario { get; set; }
        public DbSet<productos> productos { get; set; }
        public DbSet<direcciones> direcciones { get; set; }
        public DbSet<pedido> pedido { get; set; }
        public DbSet<categoria> categoria { get; set; }
        public DbSet<inventario> inventario { get; set; }
        public DbSet<detalle_pedido> detalle_pedido { get; set; }
        public DbSet<imagenes_producto> imagenes_producto { get; set; } 

    }
}
