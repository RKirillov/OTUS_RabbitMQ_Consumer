using Consumer.Settings;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var consumerNumber = int.Parse("1");
            //связь с докер физический канал связи
            var connection = GetRabbitConnection(configuration);
            //виртуальный канал, вся работа через него. потом закроем.
            var channel = connection.CreateModel();
            
            Consumers.Consumer.Register(channel, $"exchange.direct", $"queue.direct_{consumerNumber}",  $"cars.{consumerNumber}");
            //Consumers.Consumer.Register(channel, $"exchange.fanout", $"queue.fanout_{consumerNumber}",  $"cars.{consumerNumber}");
            //Consumers.Consumer.Register(channel, $"exchange.topic", $"queue.topic_{consumerNumber}",  consumerNumber > 2 ? "*.1": $"cars.{consumerNumber}");
        }
        
        private static IConnection GetRabbitConnection(IConfiguration configuration)
        {
            var rmqSettings = configuration.Get<ApplicationSettings>().RmqSettings;
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = rmqSettings.Host,
                VirtualHost = rmqSettings.VHost,
                UserName = rmqSettings.Login,
                Password = rmqSettings.Password,
            };
            IConnection conn = factory.CreateConnection();
            return conn;
        }
    }
}
