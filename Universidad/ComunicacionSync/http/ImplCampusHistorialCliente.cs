using Universidad.DTO;

namespace Universidad.ComunicacionSync.http
{
    public class ImplCampusHistorialCliente : ICampusHistorialCliente
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration configuration;
        public ImplCampusHistorialCliente(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            this.configuration = configuration;
        }
        public async Task ComunicarseConCampus(EstudianteReadDTO est)
        {
            StringContent cuerpoHttp = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(est),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            var respuesta = await _httpClient.PostAsync(configuration["CampusService"] + "/api/historial", cuerpoHttp);
            if (!respuesta.IsSuccessStatusCode)
            {
                //throw new Exception($"Error al comunicarse con el servicio de campus: {respuesta.ReasonPhrase}");
                Console.WriteLine($"Error al comunicarse con el servicio de campus: {respuesta.ReasonPhrase}");
            }
            else
            {
                Console.WriteLine("Comunicacion exitosa con el servicio de campus.");
            }
        }
    }
}
