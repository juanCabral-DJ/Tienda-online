using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hilly_StoreAPI.Data
{
    [Table("productos")]
    public class producto
    {
        [Key]
        [Column("id_producto")]
        public Guid IdProducto { get; set; }

        [Column("id_categoria")]
        public Guid? IdCategoria { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Required]
        [Column("precio", TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [Column("stock")]
        public int Stock { get; set; } = 0;

        [MaxLength(10)]
        [Column("talla")]
        public string? Talla { get; set; }

        [MaxLength(50)]
        [Column("color")]
        public string? Color { get; set; }

        [MaxLength(100)]
        [Column("marca")]
        public string? Marca { get; set; }

        [MaxLength(100)]
        [Column("material")]
        public string? Material { get; set; }

        [MaxLength(20)]
        [Column("genero")]
        public string? Genero { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Column("activo")]
        public bool Activo { get; set; } = true;

        // Navegación
        [ForeignKey("IdCategoria")]
        public virtual categoria? Categoria { get; set; }

        public virtual ICollection<imagen_producto> Imagenes { get; set; } = new List<imagen_producto>();
        public virtual ICollection<Detalle_pedido> DetallesPedido { get; set; } = new List<Detalle_pedido>();
    }
}