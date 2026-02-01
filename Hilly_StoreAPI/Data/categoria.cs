
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hilly_StoreAPI.Data
{
    [Table("categorias")]
    public class categoria
    {
        [Key]
        [Column("id_categoria")]
        public Guid IdCategoria { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        // Navegación
        public virtual ICollection<producto> Productos { get; set; } = new List<producto>();
    }
}
