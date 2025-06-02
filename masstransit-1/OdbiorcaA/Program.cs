using MassTransit;

namespace OdbiorcaA
{
    internal class Program
    {
        public static Task Handle(ConsumeContext<Komunikaty.IKomunikat> ctx)
        {
            foreach (var hdr in ctx.Headers.GetAll())
            {
                Console.WriteLine("{0}: {1}", hdr.Key, hdr.Value);
            }
            return Console.Out.WriteLineAsync($"received: {ctx.Message.tekst}");
        }

        static async Task Main(string[] args)
        {
            var bus = // cloudamqp server info hidden
                Bus.Factory.CreateUsingRabbitMq(sbc => {
                    sbc.Host(new
                    Uri("amqps:XXXX"), h => {
                        h.Username("XXXX");
                        h.Password("XXXX");
                    });
                    sbc.ReceiveEndpoint("recvqueue", ep => {
                        ep.Handler<Komunikaty.IKomunikat>(Handle);
                    });
                });
            bus.Start();
            Console.WriteLine("Odbiorca A wystartował");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
