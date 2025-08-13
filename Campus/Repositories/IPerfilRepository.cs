using Campus.Models;

namespace Campus.Repositories
{
    public interface IPerfilRepository
    {
        // Para estudiantes
        IEnumerable<Estudiante> GetEstudiantes();
        void CrearEstudiante(Estudiante estudiante);
        bool ExisteEstudiante(int ci);
        
        bool Guardar();

        bool ExisteEstudianteForaneo(int fci);
    }
}
