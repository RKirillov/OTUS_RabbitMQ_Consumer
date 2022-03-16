using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.Consumers
{
    public static class Consumer
    {
        public static void Register(IConnection connectionInfo, string name, int number)
        {
            string queueName = $"queue.{name}_{number}";
            string routingKey = $"cars.{number}";
            
            //для topic
            /*            
            if (number > 2)
            {
                routingKey = $"bicycles.{number}";
            }
            */
            using (var connection = connectionInfo)
            using (var channel = connection.CreateModel())
            {
                channel.BasicQos(0, 10, false);
                channel.QueueDeclare(queueName, false, false, false, null);
                channel.QueueBind(queueName, $"exchange.{name}", routingKey, null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (sender, e) =>
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff")} Received message");

                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    var body = e.Body;
                    var message = JsonSerializer.Deserialize<MessageDto>(Encoding.UTF8.GetString(body.ToArray()));
                    Console.WriteLine("  Received message: {0}", message.Content);
                };

                channel.BasicConsume(queueName, true, consumer);

                Console.WriteLine("Subscribed to the queue");

                Console.ReadLine();
            }
        }
    }
}