using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hilly_StoreAPI.Data
{
    [Table("usuarios")]
    public class usuarios
    {
        [Key]
        [Column("id_usuario")]
        public Guid IdUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("apellido")]
        public string Apellido { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [MaxLength(20)]
        [Column("telefono")]
        public string? Telefono { get; set; }

        [Column("direccion")]
        public string? Direccion { get; set; }

        [MaxLength(100)]
        [Column("ciudad")]
        public string? Ciudad { get; set; }

        [MaxLength(100)]
        [Column("pais")]
        public string? Pais { get; set; }

        [MaxLength(20)]
        [Column("codigo_postal")]
        public string? CodigoPostal { get; set; }

        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        [Column("ultimo_acceso")]
        public DateTime? UltimoAcceso { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [Required]
        [MaxLength(20)]
        [Column("rol")]
        public string Rol { get; set; } = "cliente";

        // Navegación
        public virtual ICollection<pedido> Pedidos { get; set; } = new List<pedido>();
    }
}
