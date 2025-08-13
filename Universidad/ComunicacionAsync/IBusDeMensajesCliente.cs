using Universidad.DTO;

namespace Universidad.ComunicacionAsync
{
    public interface IBusDeMensajesCliente
    {
        void PublicarNuevoEstudiante(EstudiantePublisherDTO est);
    }
}
