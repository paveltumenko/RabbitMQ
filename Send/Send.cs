using System;
using System.Text;
using RabbitMQ.Client;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = "Hello World!";
            Send(message);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        static void Send(string message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",
                                        routingKey: "hello",
                                        basicProperties: null,
                                        body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}
