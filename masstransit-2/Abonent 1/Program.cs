using Komunikaty;
using MassTransit;
using static MassTransit.Logging.OperationName;

namespace Abonent_1
{
    internal class Program
    {
        public static Task HandleFaultA(ConsumeContext<Fault<Komunikaty.IOdpA>> ctx)
        {
            var ex = ctx.Message.Exceptions.First();
            Console.WriteLine($"Wydawca poinformowal o bledzie!: {ex.Message}");
            return Task.CompletedTask;
        }
        public static Task Handle(ConsumeContext<IPubl> ctx)
        {
            Console.WriteLine($"Odebrano wiadomosc {ctx.Message.tekst1}");
            if (ctx.Message.numer_wiadomosci % 2 == 0)
            {
                Console.WriteLine("Odeslano wiadomosc do wydawcy");
                ctx.RespondAsync<IOdpA>(new Komunikaty.OdpA() { kto = "abonent A" });
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

                    sbc.ReceiveEndpoint("abonent1", ep => {
                        ep.Handler<IPubl>(Handle);
                        ep.Handler<Fault<Komunikaty.IOdpA>>(HandleFaultA);
                    });

                    //sbc.ReceiveEndpoint("kontroler_queue_error", ep => {
                        //ep.Handler<IPubl>(Handle);
                        //ep.Handler<Fault<Komunikaty.IOdpA>>(HandleFaultA);
                    //});
                });
            bus.Start();
            Console.WriteLine("Abonent A wystartował");
            Console.ReadKey();

        }
    }
}
