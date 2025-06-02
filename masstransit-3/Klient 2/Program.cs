using System.Reflection.Metadata;
using MassTransit;

namespace Klient_2
{
    internal class Program
    {
        public static Task Handle(ConsumeContext<Wiadomosci.PytanieoPotwierdzenie> ctx)
        {
            if (ctx.Message.login == "Klient 2")
            {
                Console.WriteLine("Czy potwierdzasz zamowienie? Wybierz T lub N.");
                if (Console.ReadKey(true).Key == ConsoleKey.T)
                {
                    ctx.RespondAsync(
                        new Wiadomosci.Potwierdzenie() { CorrelationId = ctx.Message.CorrelationId }
                    );
                    Console.WriteLine("Potwierdzono zamowienie.");
                }
                else
                {
                    ctx.RespondAsync(
                        new Wiadomosci.BrakPotwierdzenia()
                        {
                            CorrelationId = ctx.Message.CorrelationId,
                        }
                    );
                    Console.WriteLine("Odrzucono zamowienie.");
                }
            }
            return Task.CompletedTask;
        }

        public static Task Handle(ConsumeContext<Wiadomosci.AkceptacjaZamowienia> ctx)
        {
            if (ctx.Message.login == "Klient 2")
            {
                Console.WriteLine(
                    $"Zamowienie zostalo zaakceptowane przez sklep, ilosc: {ctx.Message.ilosc}"
                );
            }
            return Task.CompletedTask;
        }

        public static Task Handle(ConsumeContext<Wiadomosci.OdrzucenieZamowienia> ctx)
        {
            if (ctx.Message.login == "Klient 2")
            {
                Console.WriteLine(
                    $"Zamowienie zostalo odrzucone przez sklep, ilosc: {ctx.Message.ilosc}"
                );
            }
            return Task.CompletedTask;
        }

        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host( // cloudamqp server info hidden
                    new Uri(
                        "amqps:XXXX"
                    ),
                    h =>
                    {
                        h.Username("XXXX");
                        h.Password("XXXX");
                    }
                );
                sbc.ReceiveEndpoint(
                    "klient-2",
                    ep =>
                    {
                        ep.Handler<Wiadomosci.PytanieoPotwierdzenie>(Handle);
                        ep.Handler<Wiadomosci.AkceptacjaZamowienia>(Handle);
                        ep.Handler<Wiadomosci.OdrzucenieZamowienia>(Handle);
                    }
                );
            });
            bus.Start();
            Console.WriteLine("Klient 2 wystartowal");
            Console.WriteLine("I - Zamowienie, ESC - Wyjscie");
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.I)
                {
                    Console.WriteLine("Podaj ilosc ktora chcesz zamowic:");
                    string input = Console.ReadLine();
                    int inputInt = int.Parse(input);
                    bus.Publish(
                        new Wiadomosci.StartZamowienia() { ilosc = inputInt, login = "Klient 2" }
                    );
                    Console.WriteLine($"Wyslano zamowienie na ilosc: {inputInt}");
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            bus.Stop();
        }
    }
}
