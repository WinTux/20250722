using System.ComponentModel.DataAnnotations;

namespace Universidad.DTO
{
    public class EstudianteUpdateDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public string carrera { get; set; }
        public string? email { get; set; } // Campo opcional
    }
}
