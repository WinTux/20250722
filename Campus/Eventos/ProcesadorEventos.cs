
using Campus.DTO;
using Campus.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Campus.Eventos
{
    public class ProcesadorEventos : IProcesadorEventos
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        // Acá inyectaríamos un mapper, de ser necesario.
        public ProcesadorEventos(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public void ProcesarEvento(string tipoS)
        {
            var tipo = DeterminarEvento(tipoS);
        }

        private TipoDeEvento DeterminarEvento(string tipo)
        {
            EventoDTO tip = System.Text.Json.JsonSerializer.Deserialize<EventoDTO>(tipo.ToString());
            switch (tip.evento)
            {
                case "Estudiante_Registrado":
                    //Console.WriteLine(">> Evento Estudiante_Registrado detectado");
                    return TipoDeEvento.Estudiante_Registrado;
                case "Estudiante_Eliminado":
                    //Console.WriteLine(">> Evento Estudiante_Eliminado detectado");
                    return TipoDeEvento.Estudiante_Eliminado;
                default:
                    //Console.WriteLine(">> Evento desconocido detectado");
                    return TipoDeEvento.desconocido;
            }
        }
        private void agregarEstudiante(string mensajeEstudiantePublisher) {
            using (var scope = serviceScopeFactory.CreateScope()) {
                var repo = scope.ServiceProvider.GetRequiredService<IEstudianteRepository>();
                EstudiantePublisherDTO estudiantePub = System.Text.Json.JsonSerializer.Deserialize<EstudiantePublisherDTO>(mensajeEstudiantePublisher);
                try
                {
                    Estudiante est = mapper.Map<Estudiante>(estudiantePub);
                    if (repo.GetEstudiantePorIdentificacion(est.id) == null)
                    {
                        repo.AddEstudiante(est);
                        repo.Guardar();
                        Console.WriteLine(">> Estudiante agregado a la base de datos del Campus.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(">> Error al agregar el estudiante a la base de datos del Campus: " + e.Message);
                }
            }
        }
    }
    enum TipoDeEvento
    {
        Estudiante_Registrado,
        Estudiante_Eliminado,
        desconocido
    }
}
