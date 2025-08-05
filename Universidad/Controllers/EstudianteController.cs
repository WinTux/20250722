using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Universidad.ComunicacionSync.http;
using Universidad.DTO;
using Universidad.Models;
using Universidad.Repositories;

namespace Universidad.Controllers
{
    [Route("api/[controller]")] // http://localhost:5143/api/Estudiante
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository repo;
        private readonly IMapper mapper;
        private readonly ICampusHistorialCliente campusHistorialCliente;

        public EstudianteController(IEstudianteRepository est, IMapper mapper, ICampusHistorialCliente campusHistorialCliente)
        {
            repo = est;
            this.mapper = mapper;
            this.campusHistorialCliente = campusHistorialCliente;
        }
        // GET: api/Estudiante
        [HttpGet] // http://localhost:5143/api/Estudiante [GET]
        public ActionResult<IEnumerable<EstudianteReadDTO>> GetEstudiantes()
        {
            var estudiantes = repo.GetEstudiantes();
            return Ok(mapper.Map<IEnumerable<EstudianteReadDTO>>(estudiantes));
        }
        // GET: api/Estudiante/5
        [HttpGet("{id}", Name = "GetEstudianteById")] // http://localhost:5143/api/Estudiante/5 [GET]
        public ActionResult<EstudianteReadDTO> GetEstudianteById(int id)
        {
            var estudiante = repo.GetEstudiante(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            // Mappea al modelo Estudiante a EstudianteReadDTO
            return Ok(mapper.Map<EstudianteReadDTO>(estudiante));
        }

        // POST: api/Estudiante
        [HttpPost] // http://localhost:5143/api/Estudiante [POST]
        public ActionResult<EstudianteReadDTO> RegistrarEstudiante(EstudianteCreateDTO estudianteCreateDTO)
        {
            if (estudianteCreateDTO == null)
            {
                return BadRequest("El estudiante no puede ser nulo.");
            }
            var estudiante = mapper.Map<Estudiante>(estudianteCreateDTO);
            repo.AddEstudiante(estudiante);
            if (!repo.Guardar())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al guardar el estudiante en la base de datos.");
            }
            var estudianteReadDTO = mapper.Map<EstudianteReadDTO>(estudiante);
            // Comunicarse con el servicio de Campus para registrar el estudiante
            try
            {
                campusHistorialCliente.ComunicarseConCampus(estudianteReadDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al comunicarse con el servicio de Campus: {ex.Message}");
            }
            return CreatedAtRoute(nameof(GetEstudianteById), new { id = estudianteReadDTO.id }, estudianteReadDTO);
        }

        // PUT: api/Estudiante/5
        [HttpPut("{id}")] // http://localhost:5143/api/Estudiante/5 [PUT]
        public ActionResult ActualizarEstudiante(int id, EstudianteUpdateDTO estudianteUpdateDTO)
        {
            if (estudianteUpdateDTO == null)
            {
                return BadRequest("El estudiante no puede ser nulo.");
            }
            var estudiante = repo.GetEstudiante(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            estudianteUpdateDTO.id = id; // Aseguramos que el ID del DTO sea el mismo que el del estudiante a actualizar
            mapper.Map(estudianteUpdateDTO, estudiante);// Es una sobrecarga de Map
            repo.UpdateEstudiante(estudiante);
            if (!repo.Guardar())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el estudiante en la base de datos.");
            }
            return NoContent();
        }

        // PATCH: api/Estudiante/5
        [HttpPatch("{id}")] // http://localhost:5143/api/Estudiante/5 [PATCH]
        public ActionResult ActualizarEstudiantePorPatch(int id, JsonPatchDocument<EstudianteUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("El patch no puede ser nulo.");
            }
            var estudiante = repo.GetEstudiante(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            var estudianteUpdateDTO = mapper.Map<EstudianteUpdateDTO>(estudiante);
            patchDoc.ApplyTo(estudianteUpdateDTO, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            mapper.Map(estudianteUpdateDTO, estudiante);
            repo.UpdateEstudiante(estudiante);
            if (!repo.Guardar())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el estudiante en la base de datos.");
            }
            return NoContent();
        }
        // DELETE: api/Estudiante/5
        [HttpDelete("{id}")] // http://localhost:5143/api/Estudiante/5 [DELETE]
        public ActionResult EliminarEstudiante(int id)
        {
            var estudiante = repo.GetEstudiante(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            repo.DeleteEstudiante(estudiante);
            if (!repo.Guardar())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el estudiante en la base de datos.");
            }
            return NoContent();
        }
    }
}
