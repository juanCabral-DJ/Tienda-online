using HSW.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hilly_StoreAPI.Data
{
    [Table("detalle_pedido")]
    public class Detalle_pedido
    {
        [Key]
        [Column("id_detalle")]
        public Guid IdDetalle { get; set; }

        [Required]
        [Column("id_pedido")]
        public Guid IdPedido { get; set; }

        [Required]
        [Column("id_producto")]
        public Guid IdProducto { get; set; }

        [Required]
        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [Column("precio_unitario", TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Column("subtotal", TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }

        [MaxLength(10)]
        [Column("talla")]
        public string? Talla { get; set; }

        [MaxLength(50)]
        [Column("color")]
        public string? Color { get; set; }

        // Navegación
        [ForeignKey("IdPedido")]
        public virtual pedido Pedido { get; set; }

        [ForeignKey("IdProducto")]
        public virtual producto Producto { get; set; }
    }
}
