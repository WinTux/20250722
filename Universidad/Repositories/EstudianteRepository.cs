using Universidad.Models;

namespace Universidad.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        public Estudiante GetEstudiante(int id)
        {
            return new Estudiante
            {
                id = id,
                nombre = "Pepe",
                apellido = "Perales",
                carrera = "Ingeniería de Sistemas",
                email = "pepe.solo@gmail.com"
            };
        }

        public IEnumerable<Estudiante> GetEstudiantes()
        {
            var estudiantes = new List<Estudiante>
            {
                new Estudiante { id = 1, nombre = "Pepe", apellido = "Perales", carrera = "Ingeniería de Sistemas", email = "pepe.el.inge@gmail.com" },
                new Estudiante { id = 2, nombre = "Ana", apellido = "Sosa", carrera = "Admin. de empresas", email = "anita.inc@gmail.com" }
            };
            return estudiantes;
        }
    }
}
