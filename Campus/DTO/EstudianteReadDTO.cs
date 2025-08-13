using System.ComponentModel.DataAnnotations;

namespace Campus.DTO
{
    public class EstudianteReadDTO
    {
        public int ci { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public int telefono { get; set; }
    }
}
