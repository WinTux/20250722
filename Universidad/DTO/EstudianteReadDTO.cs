using System.ComponentModel.DataAnnotations;

namespace Universidad.DTO
{
    public class EstudianteReadDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string carrera { get; set; }
    }
}
