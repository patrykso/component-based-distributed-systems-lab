using Komunikaty;
using MassTransit;
using MassTransit.Serialization;
using System.Reflection.Metadata;
using System.Text;

namespace Kontroler
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
            var sk = new Klucz
            {
                IV = Encoding.ASCII.GetBytes(keyId.Substring(0, 16)),
                Key = Encoding.ASCII.GetBytes(k)
            };
            key = sk;
            return true;
        }
    }
    internal class Program
    {
        static bool dziala = false;
        public static Task Handle(ConsumeContext<IUstaw> ctx)
        {
            Console.WriteLine($"Odebrano wiadomosc {ctx.Message.dziala}");
            dziala = ctx.Message.dziala;
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
                    new AesCryptoStreamProvider(
                        new Dostawca("1111111111111111111111111111111"),
                        "1111111111111111");

                });
            //tsk.Wait();
            ISendEndpoint sendEp;
            //var sendEp = tsk.Result;
            await bus.StartAsync();
            var tsk = bus.GetSendEndpoint(new Uri("amqps:XXXX"));


            var exit = false;
            Console.WriteLine("Kontroler wystartował");
            Console.WriteLine("S - Start, T - Stop");


            while (!exit)
            {
                var key = Console.ReadKey(true);
                switch(key.Key)
                {
                    case ConsoleKey.S:
                        Console.WriteLine("Wystartowano wydawce");
                        //tsk.Wait(); 
                        sendEp = tsk.Result;
                        sendEp.Send<Komunikaty.IUstaw>(new Ustaw() { dziala = true });
                        await bus.Publish<IUstaw>(new Ustaw
                        {
                            dziala = true
                        }, ctx =>
                        {
                            ctx.Headers.Set(EncryptedMessageSerializer.EncryptionKeyHeader, Guid.NewGuid().ToString());
                        });
                        break;
                    case ConsoleKey.T:
                        Console.WriteLine("Zatrzymano wydawce");
                        //tsk.Wait();
                        sendEp = tsk.Result;
                        sendEp.Send<Komunikaty.IUstaw>(new Ustaw() { dziala = false });
                        await bus.Publish<IUstaw>(new Ustaw
                        {
                            dziala = false
                        }, ctx =>
                        {
                            ctx.Headers.Set(EncryptedMessageSerializer.EncryptionKeyHeader, Guid.NewGuid().ToString());
                        });
                        break;
                }
            }
        }
    }
}
