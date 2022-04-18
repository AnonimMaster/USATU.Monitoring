using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace USATU.Monitoring.Web.RabbitMQ
{
    public class RabbitMqService: IRabbitMqService
    {
        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            // Не забудьте вынести значения "localhost" и "MyQueue"
            // в файл конфигурации
            var factory = new ConnectionFactory() { Uri = new Uri("amqps://orngzufv:btLxOYMSbnZDnVDrnQP6uqRlxcQ58oMz@moose.rmq.cloudamqp.com/orngzufv") };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "MyQueue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: "MyQueue",
                    basicProperties: null,
                    body: body);
            }
        }
	}
}