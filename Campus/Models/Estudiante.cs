using System.ComponentModel.DataAnnotations;

namespace Campus.Models
{
    public class Estudiante
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string carrera { get; set; }
        public string? email { get; set; }
        // completar interfaz y hablar sobre pods, cluster ip
    }
}
