using MassTransit;
using Komunikaty;
using System.Reflection.Metadata;

namespace OdbiorcaC
{
    public class Consumer : IConsumer<IKomunikat2>
    {
        public Task Consume(ConsumeContext<IKomunikat2> ctx)
        {
            foreach (var hdr in ctx.Headers.GetAll())
            {
                Console.WriteLine("{0}: {1}", hdr.Key, hdr.Value);
            }
            Console.WriteLine($"received: {ctx.Message.tekst2}");
            return Task.CompletedTask;

        }
    }
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var instancja = new Consumer();
            var bus = // cloudamqp server info hidden
                Bus.Factory.CreateUsingRabbitMq(sbc => {
                    sbc.Host(new
                    Uri("amqps:XXXX"), h => {
                        h.Username("XXXX");
                        h.Password("XXXX");
                    });
                    sbc.ReceiveEndpoint("recvqueue", ep => {
                        ep.Instance(instancja);
                    });
                });
            bus.Start();
            Console.WriteLine("Odbiorca C wystartował");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
