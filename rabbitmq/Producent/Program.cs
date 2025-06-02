using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Producent
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory() // cloudamqp server info hidden
            {
                HostName = "hawk.rmq.cloudamqp.com",
                UserName = "XXXX",
                Password = "XXXX",
                VirtualHost = "XXXX"
            };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();
            var tozsamosc = "Nadawca";

            using (connection)
            using (channel)
            {
                //await channel.QueueDeclareAsync("message_queue", false, false, false, null);

                // zad 7
                await channel.ExchangeDeclareAsync("top_rout", "topic", false, false, null);

                // zad 6
                //string replyQueueName = channel.QueueDeclareAsync().Result.QueueName;
                //var corrId = Guid.NewGuid().ToString();

                //var consumer = new AsyncEventingBasicConsumer(channel);
                //consumer.ReceivedAsync += (model, ea) =>
                //{
                //    if (ea.BasicProperties.CorrelationId == corrId)
                //    {
                //        var response = Encoding.UTF8.GetString(ea.Body.ToArray());
                //        Console.WriteLine($"Nadawca: Odpowiedz od odbiorcy: {response}");
                //    }
                //    return Task.CompletedTask;
                //};
                //await channel.BasicConsumeAsync(replyQueueName, true, consumer);

                var routingKey = string.Empty;

                for (int i = 1; i <= 10; i++)
                {
                    if (i % 2 == 0)
                    {
                        routingKey = "abc.xyz";
                    }
                    else
                    {
                        routingKey = "abc.def";
                    }

                    string message = $"message no. {i}; routingKey: {routingKey}";
                    var body = Encoding.UTF8.GetBytes(message);

                    var props = new BasicProperties();
                    props.Headers = new Dictionary<string, object>();
                    props.Headers.Add("job sec", 10);
                    props.Headers.Add("another header", 20);
                    // zad 6
                    //props.CorrelationId = corrId;
                    //props.ReplyTo = replyQueueName;



                    await channel.BasicPublishAsync(
                            exchange: "top_rout",
                            //routingKey: "message_queue",
                            routingKey: routingKey,
                            mandatory: false,
                            basicProperties: props,
                            body: body
                        );
                    Console.WriteLine($"{tozsamosc}: {message}");
                }

                Console.ReadKey();
            }
        }
    }
}
