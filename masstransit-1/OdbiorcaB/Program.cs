using MassTransit;
using Komunikaty;
using System.Reflection.Metadata;

namespace OdbiorcaB
{
    public class Consumer : IConsumer<Komunikaty.Komunikat3>
    {
        private int counter = 0;
        public Task Consume(ConsumeContext<Komunikaty.Komunikat3> ctx)
        {
            counter++;
            foreach (var hdr in ctx.Headers.GetAll())
            {
                Console.WriteLine("{0}: {1}", hdr.Key, hdr.Value);
            }
            Console.WriteLine($"(type 3) received: {ctx.Message.tekst3}");
            Console.WriteLine($"received: {ctx.Message.tekst3}");
            Console.WriteLine($"received: {ctx.Message.tekst3}");
            Console.WriteLine($"Licznik: {counter}");
            return Task.CompletedTask;

        }
    }
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var instancja = new Consumer();
            var bus =  // cloudamqp server info hidden
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
            Console.WriteLine("Odbiorca B wystartował");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
