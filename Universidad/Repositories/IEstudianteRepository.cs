using Universidad.Models;

namespace Universidad.Repositories
{
    public interface IEstudianteRepository
    {
        IEnumerable<Estudiante> GetEstudiantes();
        Estudiante GetEstudiante(int id);
        void AddEstudiante(Estudiante estudiante);

        bool Guardar();
    }
}
