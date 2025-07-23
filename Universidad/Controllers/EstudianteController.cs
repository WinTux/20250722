using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universidad.Models;
using Universidad.Repositories;

namespace Universidad.Controllers
{
    [Route("api/[controller]")] // http://localhost:5143/api/Estudiante
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository repo;
        public EstudianteController(IEstudianteRepository est)
        {
            repo = est;
        }
        // GET: api/Estudiante
        [HttpGet] // http://localhost:5143/api/Estudiante [GET]
        public ActionResult<IEnumerable<Estudiante>> GetEstudiantes()
        {
            var estudiantes = repo.GetEstudiantes();
            return Ok(estudiantes);
        }
        // GET: api/Estudiante/5
        [HttpGet("{id}")] // http://localhost:5143/api/Estudiante/5 [GET]
        public ActionResult<Estudiante> GetEstudianteById(int id)
        {
            var estudiante = repo.GetEstudiante(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return Ok(estudiante);
        }
    }
}
