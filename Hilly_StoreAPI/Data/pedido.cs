using HSW.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hilly_StoreAPI.Data
{
    [Table("pedidos")]
    public class pedido
    {
        [Key]
        [Column("id_pedido")]
        public Guid IdPedido { get; set; }

        [Required]
        [Column("id_usuario")]
        public Guid IdUsuario { get; set; }

        [Column("fecha_pedido")]
        public DateTime FechaPedido { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        [Column("estado")]
        public string Estado { get; set; } = "pendiente";

        [Required]
        [Column("total", TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Required]
        [Column("direccion_envio")]
        public string DireccionEnvio { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("ciudad_envio")]
        public string CiudadEnvio { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("pais_envio")]
        public string PaisEnvio { get; set; }

        [MaxLength(20)]
        [Column("codigo_postal_envio")]
        public string? CodigoPostalEnvio { get; set; }

        [Column("notas")]
        public string? Notas { get; set; }

        [Column("fecha_actualizacion")]
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

        // Navegación
        [ForeignKey("IdUsuario")]
        public virtual usuarios Usuario { get; set; }

        public virtual ICollection<Detalle_pedido> Detalles { get; set; } = new List<Detalle_pedido>();
    }
}
