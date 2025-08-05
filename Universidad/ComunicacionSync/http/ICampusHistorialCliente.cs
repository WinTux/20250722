using Universidad.DTO;

namespace Universidad.ComunicacionSync.http
{
    public interface ICampusHistorialCliente
    {
        Task ComunicarseConCampus(EstudianteReadDTO est);
    }
}
