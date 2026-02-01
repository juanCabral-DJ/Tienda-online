using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hilly_StoreAPI.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<usuarios> Usuarios { get; set; }
        public DbSet<categoria> Categorias { get; set; }
        public DbSet<producto> Productos { get; set; }
        public DbSet<imagen_producto> ImagenesProducto { get; set; }
        public DbSet<pedido> Pedidos { get; set; }
        public DbSet<Detalle_pedido> DetallesPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Usuario
            modelBuilder.Entity<usuarios>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Activo);

                entity.Property(e => e.Rol)
                    .HasConversion<string>();
            });

            // Configuración de Categoria
            modelBuilder.Entity<categoria>(entity =>
            {
                entity.HasIndex(e => e.Nombre).IsUnique();
            });

            // Configuración de Producto
            modelBuilder.Entity<producto>(entity =>
            {
                entity.HasIndex(e => e.IdCategoria);
                entity.HasIndex(e => e.Activo);
                entity.HasIndex(e => e.Precio);

                entity.HasOne(p => p.Categoria)
                    .WithMany(c => c.Productos)
                    .HasForeignKey(p => p.IdCategoria)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.Property(e => e.Precio)
                    .HasPrecision(10, 2);
            });

            // Configuración de ImagenProducto
            modelBuilder.Entity<imagen_producto>(entity =>
            {
                entity.HasIndex(e => e.IdProducto);
                entity.HasIndex(e => new { e.IdProducto, e.EsPrincipal });

                entity.HasOne(i => i.Producto)
                    .WithMany(p => p.Imagenes)
                    .HasForeignKey(i => i.IdProducto)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Pedido
            modelBuilder.Entity<pedido>(entity =>
            {
                entity.HasIndex(e => e.IdUsuario);
                entity.HasIndex(e => e.Estado);
                entity.HasIndex(e => e.FechaPedido);

                entity.HasOne(p => p.Usuario)
                    .WithMany(u => u.Pedidos)
                    .HasForeignKey(p => p.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Total)
                    .HasPrecision(10, 2);
            });

            // Configuración de DetallePedido
            modelBuilder.Entity<Detalle_pedido>(entity =>
            {
                entity.HasIndex(e => e.IdPedido);
                entity.HasIndex(e => e.IdProducto);

                entity.HasOne(d => d.Pedido)
                    .WithMany(p => p.Detalles)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.DetallesPedido)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.PrecioUnitario)
                    .HasPrecision(10, 2);

                entity.Property(e => e.Subtotal)
                    .HasPrecision(10, 2);
            });
        }

        /// <summary>
        /// Método para calcular automáticamente el subtotal antes de guardar
        /// (simula el trigger de PostgreSQL)
        /// </summary>
        public override int SaveChanges()
        {
            CalcularSubtotales();
            ActualizarFechas();
            return base.SaveChanges();
        }

        /// <summary>
        /// Método asíncrono para calcular automáticamente el subtotal antes de guardar
        /// </summary>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            CalcularSubtotales();
            ActualizarFechas();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Calcula automáticamente los subtotales de los detalles de pedido
        /// (Replica el trigger trigger_calcular_subtotal)
        /// </summary>
        private void CalcularSubtotales()
        {
            var detalles = ChangeTracker.Entries<Detalle_pedido>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var detalle in detalles)
            {
                detalle.Entity.Subtotal = detalle.Entity.Cantidad * detalle.Entity.PrecioUnitario;
            }
        }

        /// <summary>
        /// Actualiza la fecha de modificación de los pedidos
        /// (Replica el trigger trigger_actualizar_pedido)
        /// </summary>
        private void ActualizarFechas()
        {
            var pedidos = ChangeTracker.Entries<pedido>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var pedido in pedidos)
            {
                pedido.Entity.FechaActualizacion = DateTime.UtcNow;
            }
        }
    }
}
