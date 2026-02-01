using HSW.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hilly_StoreAPI.Data
{
    [Table("imagenes_producto")]
    public class imagen_producto
    {
        [Key]
        [Column("id_imagen")]
        public Guid IdImagen { get; set; }

        [Required]
        [Column("id_producto")]
        public Guid IdProducto { get; set; }

        [Required]
        [MaxLength(500)]
        [Column("ruta_imagen")]
        public string RutaImagen { get; set; }

        [Column("es_principal")]
        public bool EsPrincipal { get; set; } = false;

        [Column("orden")]
        public int Orden { get; set; } = 0;

        [Column("fecha_subida")]
        public DateTime FechaSubida { get; set; } = DateTime.UtcNow;

        // Navegación
        [ForeignKey("IdProducto")]
        public virtual producto Producto { get; set; }
    }

}
