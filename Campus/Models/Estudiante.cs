using System.ComponentModel.DataAnnotations;

namespace Campus.Models
{
    public class Estudiante
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public int fci { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        public string carrera { get; set; }
        public string? email { get; set; }
        // completar interfaz y hablar sobre pods, cluster ip
    }
}
