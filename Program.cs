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

            //Consumers.Consumer.Register(GetRabbitConnection(), "direct", int.Parse(args[0])); 
            //Consumers.Consumer.Register(GetRabbitConnection(), "fanout", int.Parse(args[0]));
            //Consumers.Consumer.Register(GetRabbitConnection(), "topic", int.Parse(args[0]));
            Consumers.Consumer.Register(GetRabbitConnection(configuration), "direct.instant", int.Parse(args[0]));
        }

        static private IConnection GetRabbitConnection(IConfiguration configuration)
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
