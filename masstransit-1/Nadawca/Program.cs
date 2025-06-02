using MassTransit;
using Komunikaty;
using System.Reflection.Metadata;

namespace Nadawca
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var bus = // cloudamqp server info hidden
                Bus.Factory.CreateUsingRabbitMq(sbc => {
                sbc.Host(new
                Uri("amqps:XXXX"), h => {
                    h.Username("XXXXX");
                    h.Password("XXXXX");
                });
            });
            bus.Start();
            Console.WriteLine("Nadawca wystartował");

            for (int i = 0; i < 10; i++)
            {

                //bus.Publish(new Komunikaty.Komunikat() { tekst = $"message no {i}" });
                //bus.Publish(new Komunikaty.Komunikat() { tekst = $"message no {i}" }, ctx => {
                    //ctx.Headers.Set("klucz1", "wartosc1");
                    //ctx.Headers.Set("klucz2", "wartosc2");
                //});

                //bus.Publish(new Komunikaty.Komunikat2() { tekst2 = $"message (type 2) no {i}" }, ctx => {
                    //ctx.Headers.Set("klucz1", "wartosc1");
                    //ctx.Headers.Set("klucz2", "wartosc2");
                //});

                bus.Publish(new Komunikaty.Komunikat3() { 
                    tekst3 = $"message (type 3) no {i}", 
                    tekst2 = $"message (type 2) no {i}",
                    tekst = $"message no {i}"
                }, 
                    ctx => {
                        ctx.Headers.Set("klucz1", "wartosc1");
                        ctx.Headers.Set("klucz2", "wartosc2");
                    });
                Console.WriteLine("Nadawca wyslal wiadomosc no ", i);
            }


            Console.ReadKey();
            bus.Stop();
        }
    }
}
