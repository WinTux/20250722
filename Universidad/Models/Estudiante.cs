using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universidad.Models
{
    [Table("Estudiante")]
    public class Estudiante
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public string carrera { get; set; }
        [MaxLength(50)]
        public string email { get; set; }
    }
}
