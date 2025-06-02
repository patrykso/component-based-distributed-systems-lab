using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Konsument_2
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

            // zad 4
            await channel.BasicQosAsync(0, 1, false);

            using (connection)
            using (channel)
            {
                //await channel.QueueDeclareAsync("message_queue", false, false, false, null);

                // zad 7
                await channel.ExchangeDeclareAsync("top_rout", "topic", false, false, null);
                var queueDeclare = await channel.QueueDeclareAsync();
                var queueName = queueDeclare.QueueName;
                await channel.QueueBindAsync(queueName, "top_rout", "*.xyz");


                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Odbiorca 2: Odebrano wiadomosc: {message}");

                    // zad 3
                    if (ea.BasicProperties.Headers != null)
                    {
                        int jobSec = (int)ea.BasicProperties.Headers["job sec"];
                        int anotherHeader = (int)ea.BasicProperties.Headers["another header"];
                        Console.WriteLine($"Odbiorca 2: Odebrane naglowki: {jobSec} oraz {anotherHeader}");
                    }

                    // zad 6
                    if (!string.IsNullOrEmpty(ea.BasicProperties.ReplyTo))
                    {
                        var responseMessage = $"Odbiorca 2 odpowiada: {DateTime.Now.ToLongTimeString()}";
                        var responseBytes = Encoding.UTF8.GetBytes(responseMessage);

                        var replyProps = new BasicProperties();
                        replyProps.CorrelationId = ea.BasicProperties.CorrelationId;

                        await channel.BasicPublishAsync(
                            exchange: "",
                            routingKey: ea.BasicProperties.ReplyTo,
                            mandatory: false,
                            basicProperties: replyProps,
                            body: responseBytes
                        );
                    }

                    // zad 5
                    await Task.Delay(500);
                    await channel.BasicAckAsync(ea.DeliveryTag, false);

                };

                //await channel.BasicConsumeAsync("message_queue", autoAck: false, consumer: consumer);
                await channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer);
                Console.ReadKey();
            }
        }
    }
}