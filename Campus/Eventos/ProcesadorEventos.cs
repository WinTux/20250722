
using AutoMapper;
using Campus.DTO;
using Campus.Models;
using Campus.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json;

namespace Campus.Eventos
{
    public class ProcesadorEventos : IProcesadorEventos
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IMapper mapper;

        // Acá inyectaríamos un mapper, de ser necesario.
        public ProcesadorEventos(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.mapper = mapper;
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
            using (var alcance = serviceScopeFactory.CreateScope())
            {
                var repo = alcance.ServiceProvider.GetRequiredService<IPerfilRepository>();
                var estudiantePublisherDTO = JsonSerializer.Deserialize<EstudiantePublisherDTO>(mensajeEstudiantePublisher);
                try
                {
                    var est = mapper.Map<Estudiante>(estudiantePublisherDTO);
                    if (!repo.ExisteEstudianteForaneo(est.fci))
                    {
                        repo.CrearEstudiante(est);
                        repo.Guardar();
                    }
                    else
                    {
                        Console.WriteLine($"El estudiante {est.fci} ya existe en la base de datos.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error al agregar el estudiante: {e.Message}");
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
