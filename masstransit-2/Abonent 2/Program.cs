using Komunikaty;
using MassTransit;

namespace Abonent_2
{
    internal class Program
    {
        public static Task HandleFaultB(ConsumeContext<Fault<Komunikaty.IOdpB>> ctx)
        {
            var ex = ctx.Message.Exceptions.First();
            Console.WriteLine($"Wydawca poinformowal o bledzie!: {ex.Message}");
            return Task.CompletedTask;
        }
        public static Task Handle(ConsumeContext<IPubl> ctx)
        {
            Console.WriteLine($"Odebrano wiadomosc {ctx.Message.tekst1}");
            if (ctx.Message.numer_wiadomosci % 3 == 0)
            {
                Console.WriteLine("Odeslano wiadomosc do wydawcy");
                ctx.RespondAsync<IOdpB>(new Komunikaty.OdpB() { kto = "abonent B" });
            }
            return Task.CompletedTask;
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

                    sbc.ReceiveEndpoint("abonent2", ep => {
                        ep.Handler<IPubl>(Handle);
                        ep.Handler<Fault<IOdpB>>(HandleFaultB);
                    });

                    //sbc.ReceiveEndpoint("kontroler_queue2_error", ep => {
                        //ep.Handler<IPubl>(Handle);
                        //ep.Handler<Fault<IOdpB>>(HandleFaultB);
                    //});
                });
            bus.Start();
            Console.WriteLine("Abonent B wystartował");
            Console.ReadKey();
        }
    }
}
