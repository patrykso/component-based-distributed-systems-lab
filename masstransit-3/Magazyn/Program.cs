using MassTransit;

namespace Magazyn
{
    internal class Program
    {
        private static int zarezerwowane;
        private static int dostepne;

        public static Task Handle(ConsumeContext<Wiadomosci.IPytanieoWolne> ctx)
        {
            Console.WriteLine($"Otrzymano zapytanie o wolne w ilosci: {ctx.Message.ilosc}");

            if (ctx.Message.ilosc <= dostepne)
            {
                dostepne -= ctx.Message.ilosc;
                zarezerwowane += ctx.Message.ilosc;
                Console.Out.WriteLine($"Zarezerwowano {ctx.Message.ilosc} sztuk");
                ctx.RespondAsync<Wiadomosci.IOdpowiedzWolne>(
                    new Wiadomosci.OdpowiedzWolne() { CorrelationId = ctx.Message.CorrelationId }
                );
            }
            else
            {
                Console.Out.WriteLine($"Brak wolnych sztuk w ilosci: {ctx.Message.ilosc}");
                ctx.RespondAsync<Wiadomosci.IOdpowiedzWolneNegatywna>(
                    new Wiadomosci.OdpowiedzWolneNegatywna()
                    {
                        CorrelationId = ctx.Message.CorrelationId,
                    }
                );
                // klient zamawia za duzo sztuk -> odrzucenie zamowienia -> magazyn dostaje odrzucenie, ktore obsluguje -> za chwile zwiekszy ilosc dostepnych i zarezerwowanych
                dostepne -= ctx.Message.ilosc;
                zarezerwowane += ctx.Message.ilosc;
            }
            return Task.CompletedTask;
        }

        public static Task Handle(ConsumeContext<Wiadomosci.IAkceptacjaZamowienia> ctx)
        {
            Console.WriteLine(
                $"Zamowienie dla {ctx.Message.login} na ilosc: {ctx.Message.ilosc} zaakceptowano"
            );
            zarezerwowane -= ctx.Message.ilosc;
            return Task.CompletedTask;
        }

        public static Task Handle(ConsumeContext<Wiadomosci.IOdrzucenieZamowienia> ctx)
        {
            Console.WriteLine(
                $"Zamowienie dla {ctx.Message.login} na ilosc: {ctx.Message.ilosc} odrzucono"
            );
            dostepne += ctx.Message.ilosc;
            zarezerwowane -= ctx.Message.ilosc;
            return Task.CompletedTask;
        }

        static async Task Main(string[] args)
        {
            zarezerwowane = 0;
            dostepne = 0;

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
                    "magazyn",
                    ep =>
                    {
                        ep.Handler<Wiadomosci.IPytanieoWolne>(Handle);
                        ep.Handler<Wiadomosci.IAkceptacjaZamowienia>(Handle);
                        ep.Handler<Wiadomosci.IOdrzucenieZamowienia>(Handle);
                    }
                );
            });
            bus.Start();
            Console.WriteLine("Magazyn wystartowal");
            Console.WriteLine("S - Stan, I - Dodaj do magazynu, ESC - Wyjscie");
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.S)
                {
                    Console.WriteLine(
                        $"Stan magazynu: dostepne: {dostepne} zarezerwowane: {zarezerwowane}"
                    );
                }
                else if (key == ConsoleKey.I)
                {
                    Console.WriteLine("Podaj liczbe o ktora chcesz zwiekszyc stan magazynu:");
                    string input = Console.ReadLine();
                    int inputInt = int.Parse(input);
                    dostepne += inputInt;
                    Console.WriteLine(
                        $"Stan magazynu: dostepne: {dostepne} zarezerwowane: {zarezerwowane}"
                    );
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
