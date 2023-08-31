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
        public static void Register(IModel model, string exchangeName, string queueName, string routingKey)
        {
                model.BasicQos(0, 10, false);
                model.QueueDeclare(queueName, false, false, false, null);
                model.QueueBind(queueName, exchangeName, routingKey, null);

                var consumer = new EventingBasicConsumer(model);

                consumer.Received += (sender, e) =>
                {
                    //throw new Exception("Error has occured");
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff")} Received message");
                    var body = e.Body;
                    var message = JsonSerializer.Deserialize<MessageDto>(Encoding.UTF8.GetString(body.ToArray()));
                    Console.WriteLine("  Received message: {0}", message.Content);
                    model.BasicAck(e.DeliveryTag, false);
                    
                    Thread.Sleep(TimeSpan.FromSeconds(2)); // Имитация долгой обработки
                };

                model.BasicConsume(queueName, false, consumer);

                Console.WriteLine($"Subscribed to the queue with key {routingKey}");

                Console.ReadLine();
        }
    }
}