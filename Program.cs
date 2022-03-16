using RabbitMQ.Client;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Consumers.Consumer.Register(GetRabbitConnection(), "direct", int.Parse(args[0]));
            //Consumers.Consumer.Register(GetRabbitConnection(), "fanout", int.Parse(args[0]));
            //Consumers.Consumer.Register(GetRabbitConnection(), "topic", int.Parse(args[0]));
            //Consumers.Consumer.Register(GetRabbitConnection(), "direct.instant", int.Parse(args[0]));
        }

        static private IConnection GetRabbitConnection()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "ykziztbb",
                Password = "oZaUpy2Sru1P0b04K9ghjx3MSFpXTMIU",
                VirtualHost = "ykziztbb",
                HostName = "hawk.rmq.cloudamqp.com"
            };
            IConnection conn = factory.CreateConnection();
            return conn;
        }
    }
}
