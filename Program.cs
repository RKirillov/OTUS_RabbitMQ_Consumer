using RabbitMQ.Client;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Consumers.Consumer.Register(GetRabbitConnection(), "direct", int.Parse(args[0]));
            //Consumers.Consumer.Register(GetRabbitConnection(), "fanout", int.Parse(args[0]));
            //Consumers.Consumer.Register(GetRabbitConnection(), "topic", int.Parse(args[0]));
            Consumers.Consumer.Register(GetRabbitConnection(), "direct.instant", int.Parse(args[0]));
        }

        static private IConnection GetRabbitConnection()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "xvvcjzoi",
                Password = "3zzqgto8t6iqz6EMWhrx3fj8ubnToHJ6",
                VirtualHost = "xvvcjzoi",
                HostName = "cow.rmq2.cloudamqp.com"
            };
            IConnection conn = factory.CreateConnection();
            return conn;
        }
    }
}
