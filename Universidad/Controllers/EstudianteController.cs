using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public EstudianteController(IEstudianteRepository est, IMapper mapper)
        {
            repo = est;
            this.mapper = mapper;
        }
        // GET: api/Estudiante
        [HttpGet] // http://localhost:5143/api/Estudiante [GET]
        public ActionResult<IEnumerable<EstudianteReadDTO>> GetEstudiantes()
        {
            var estudiantes = repo.GetEstudiantes();
            return Ok(mapper.Map<IEnumerable<EstudianteReadDTO>>(estudiantes));
        }
        // GET: api/Estudiante/5
        [HttpGet("{id}")] // http://localhost:5143/api/Estudiante/5 [GET]
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
            return CreatedAtAction(nameof(GetEstudianteById), new { id = estudianteReadDTO.id }, estudianteReadDTO);

            // Hablar sobre este return
        }
    }
}
