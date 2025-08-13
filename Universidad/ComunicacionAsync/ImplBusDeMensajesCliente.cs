using RabbitMQ.Client;
using Universidad.DTO;


namespace Universidad.ComunicacionAsync
{
    public class ImplBusDeMensajesCliente : IBusDeMensajesCliente
    {
        private readonly IConfiguration configuration;
        private readonly IConnection conexion;
        private readonly IModel canal;
        public ImplBusDeMensajesCliente(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = configuration["Host_RabbitMQ"],
                Port = int.Parse(configuration["Puerto_RabbitMQ"])
            };
            try {
                conexion = factory.CreateConnection();
                canal = conexion.CreateModel();
                canal.ExchangeDeclare(
                    exchange: "universidad_eventos",
                    type: ExchangeType.Fanout // Tipo de intercambio Fanout para publicar a todos los consumidores
                );
                // Opcional
                conexion.ConnectionShutdown += RabbitMQ_evento_shutdown;
            }
            catch (Exception e) { 
                Console.WriteLine("Error al conectar con RabbitMQ: " + e.Message);
            }
        }
        public void PublicarNuevoEstudiante(EstudiantePublisherDTO est)
        {
            string mensaje = System.Text.Json.JsonSerializer.Serialize(est);
            if (conexion.IsOpen)
                Enviar(mensaje);
            else
                Console.WriteLine("No se pudo enviar el mensaje, la conexión RabbitMQ está cerrada.");
        }

        private void Enviar(string mensaje)
        {
            var cuerpo = System.Text.Encoding.UTF8.GetBytes(mensaje);
            canal.BasicPublish(
                exchange: "universidad_eventos",
                routingKey: "", // No se usa en Fanout
                basicProperties: null,
                body: cuerpo
            );
            Console.WriteLine(">> Mensaje enviado a RabbitMQ: " + mensaje);
        }

        public void RabbitMQ_evento_shutdown(object sender, ShutdownEventArgs args) {
            // Algo de interes podría ejecutarse acá
            Console.WriteLine("RabbitMQ connection shutdown: " + args.ReplyText);
            Finalizar();
        }
        private void Finalizar() { 
            if(canal.IsOpen) {
                canal.Close();
                conexion.Close();
            }
        }
    }
}
