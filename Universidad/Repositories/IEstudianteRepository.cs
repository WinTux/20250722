using Universidad.Models;

namespace Universidad.Repositories
{
    public interface IEstudianteRepository
    {
        IEnumerable<Estudiante> GetEstudiantes();
        Estudiante GetEstudiante(int id);
        void AddEstudiante(Estudiante estudiante);
        void UpdateEstudiante(Estudiante estudiante);
        void DeleteEstudiante(Estudiante estudiante);
        bool Guardar();
    }
}
