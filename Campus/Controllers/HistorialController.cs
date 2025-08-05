using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Campus.Controllers
{
    [Route("api/historial")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Cosa 1","Cosa 2","Cosa 3"};
        }
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Cosa {id}";
        }
        [HttpPost]
        public ActionResult Post()
        {
            Console.WriteLine("Llegó una petición por POST");
            Debug.WriteLine("Llegó una petición por POST");
            return Ok("Post recibido correctamente en HistorialController");
        }
    }
}
