using System.ComponentModel.DataAnnotations;

namespace Campus.DTO
{
    public class EstudianteCreateDTO
    {
        [Required]
        public int ci { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [EmailAddress]
        public string correo { get; set; }
        [Required]
        public int telefono { get; set; }
    }
}
