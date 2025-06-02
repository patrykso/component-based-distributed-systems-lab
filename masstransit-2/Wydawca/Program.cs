using Komunikaty;
using MassTransit;
using System;
using MassTransit.Serialization;
using System.Text;

namespace Wydawca
{
    public class Klucz : SymmetricKey
    {
        public byte[] IV { get; set; }
        public byte[] Key { get; set; }
    }
    public class Dostawca : ISymmetricKeyProvider
    {
        private string k; public Dostawca(string _k) { k = _k; }
        public bool TryGetKey(string keyId, out SymmetricKey key)
        {
            var sk = new Klucz {
                IV = Encoding.ASCII.GetBytes(keyId.Substring(0, 16)),
                Key = Encoding.ASCII.GetBytes(k)
            };
            key = sk;
            return true;
        }
    }
    public class ConsumeObserver : IConsumeObserver
    {
        public int odp_tried = 0;
        public int odp_success = 0;

        Task IConsumeObserver.ConsumeFault<T>(ConsumeContext<T> context, Exception exception)
        {
            return Task.CompletedTask;
        }

        Task IConsumeObserver.PostConsume<T>(ConsumeContext<T> context)
        {
            //if (typeof(T) == typeof(OdpA))
            //{
            //    Interlocked.Increment(ref odpa_success);
            //} else if (typeof(T) == typeof(OdpB))
            //{
            //    Interlocked.Increment(ref odpb_success);
            //}
            Interlocked.Increment(ref odp_success);
            return Task.CompletedTask;
        }

        Task IConsumeObserver.PreConsume<T>(ConsumeContext<T> context)
        {
        //    if (typeof(T) == typeof(OdpA))
        //    {
        //        Interlocked.Increment(ref odpa_tried);
        //}
        //    else if (typeof(T) == typeof(OdpB))
        //    {
        //        Interlocked.Increment(ref odpb_tried);
        //    }
            Interlocked.Increment(ref odp_tried);
            return Task.CompletedTask;
        }
    }
    public class PublishObserver : IPublishObserver
    {
        public int opublikowane = 0;
        Task IPublishObserver.PostPublish<T>(PublishContext<T> context)
        {
            Interlocked.Increment(ref opublikowane);
            return Task.CompletedTask;
        }

        Task IPublishObserver.PrePublish<T>(PublishContext<T> context)
        {
            return Task.CompletedTask;
        }

        Task IPublishObserver.PublishFault<T>(PublishContext<T> context, Exception exception)
        {
            return Task.CompletedTask;
        }
    }
    internal class Program
    {
        static bool dziala = false;
        static Random rnd = new Random();
        public static Task Handle(ConsumeContext<IUstaw> ctx)
        {
            Console.WriteLine($"Odebrano wiadomosc {ctx.Message.dziala}");
            dziala = ctx.Message.dziala;
            return Task.CompletedTask;
        }

        public static Task Handle(ConsumeContext<IOdpA> ctx)
        {
            if (rnd.Next(0, 2) == 0)
            {
                Console.WriteLine($"Odebrano wiadomość: {ctx.Message.kto}");
            }
            else
            {
                Console.WriteLine("Wyslano wyjatek abonentowi A!");
                throw new Exception("WYJATEK (abonent A)");
            }
            return Task.CompletedTask;
        }

        public static Task Handle(ConsumeContext<IOdpB> ctx)
        {
            if (rnd.Next(0, 2) == 0)
            {
                Console.WriteLine($"Odebrano wiadomość: {ctx.Message.kto}");
            }
            else
            {
                Console.WriteLine("Wyslano wyjatek abonentowi B!");
                throw new Exception("WYJATEK (abonent B)");
            }
            return Task.CompletedTask;
        }
        static async Task Main(string[] args)
        {
            var cobserver = new ConsumeObserver();
            var pobserver = new PublishObserver();
            var bus = // cloudamqp server info hidden
                Bus.Factory.CreateUsingRabbitMq(sbc => {
                    sbc.Host(new
                    Uri("amqps:XXXX"), h => {
                        h.Username("XXXX");
                        h.Password("XXXX");
                    });

                    new AesCryptoStreamProvider(
                        new Dostawca("19171119171119171119171119171111"),
                        "1917111917111917");


                    sbc.ReceiveEndpoint("kontroler_queue", ep => {
                        ep.UseRetry(r => r.Immediate(3));
                        ep.Handler<Komunikaty.IUstaw>(Handle);
                        ep.Handler<Komunikaty.IOdpA>(Handle);
                        ep.Handler<Komunikaty.IOdpB>(Handle);
                    });
                });
            bus.ConnectConsumeObserver(cobserver);
            bus.ConnectPublishObserver(pobserver);
            bus.Start();
            Console.WriteLine("Wydawca wystartował");
            Console.WriteLine("S - statystyki");

            var exit = false;
            var counter = 0;

            _ = Task.Run(() =>
            {
                while (Console.ReadKey(true).Key == ConsoleKey.S)
                {
                    Console.WriteLine("Statystyki");
                    Console.WriteLine($"Prob obsluzenia: {cobserver.odp_tried}");
                    Console.WriteLine($"Pomyslnie obsluzone: {cobserver.odp_success}");
                    Console.WriteLine($"Opublikowane komunikaty: {pobserver.opublikowane}");
                }

            });


            while (!exit)
            {
                if (!dziala)
                {
                    Console.WriteLine("Wydawca nie dziala");

                }
                else if (dziala)
                {
                    var komunikat = new Publ() { tekst1 = $"Wiadomosc numer {counter++}", numer_wiadomosci = counter};
                    await bus.Publish<IPubl>(komunikat);
                    Console.WriteLine($"Nadano wiadomosc: {komunikat}");
                }
                await Task.Delay(1000);
            }

        }
    }
}
