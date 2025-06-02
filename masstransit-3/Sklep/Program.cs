using System.Reflection.Metadata;
using MassTransit;

namespace Sklep
{
    public class ZamowienieDane : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public int ilosc { get; set; }
        public string login { get; set; }
        public Guid? timeoutId { get; set; }
    }

    public class ZamowienieSaga : MassTransitStateMachine<ZamowienieDane>
    {
        public State PotwierdzoneKlient { get; private set; }
        public State PotwierdzoneMagazyn { get; private set; }
        public State Niepotwierdzone { get; private set; }

        public Event<Wiadomosci.IStartZamowienia> StartZamowienia { get; private set; }
        public Event<Wiadomosci.IPotwierdzenie> PotwierdzenieKlienta { get; private set; }
        public Event<Wiadomosci.IBrakPotwierdzenia> BrakPotwierdzeniaKlienta { get; private set; }
        public Event<Wiadomosci.IOdpowiedzWolne> OdpowiedzWolne { get; private set; }
        public Event<Wiadomosci.IOdpowiedzWolneNegatywna> OdpowiedzWolneNegatywna
        {
            get;
            private set;
        }

        //public Event<Wiadomosci.Timeout> TimeoutEvt { get; private set; }
        public Schedule<ZamowienieDane, Wiadomosci.Timeout> TO { get; private set; }

        public ZamowienieSaga()
        {
            InstanceState(x => x.CurrentState);

            Schedule(
                () => TO,
                x => x.timeoutId,
                x =>
                {
                    x.Delay = TimeSpan.FromSeconds(10);
                }
            );

            Event(
                () => StartZamowienia,
                x =>
                    x.CorrelateBy(s => s.login, ctx => ctx.Message.login)
                        .SelectId(context => Guid.NewGuid())
            );

            Initially(
                When(StartZamowienia)
                    .Then(context =>
                    {
                        context.Saga.ilosc = context.Message.ilosc;
                        context.Saga.login = context.Message.login;
                    })
                    .ThenAsync(ctx =>
                    {
                        return Console.Out.WriteLineAsync(
                            $"START ZAMOWIENIA: login={ctx.Message.login}, ilosc {ctx.Saga.ilosc} i id={ctx.Saga.CorrelationId}"
                        );
                    })
                    .Respond(ctx =>
                    {
                        return new Wiadomosci.PytanieoPotwierdzenie
                        {
                            CorrelationId = ctx.Saga.CorrelationId,
                            ilosc = ctx.Message.ilosc,
                            login = ctx.Message.login,
                        };
                    })
                    .Respond(ctx =>
                    {
                        return new Wiadomosci.PytanieoWolne
                        {
                            CorrelationId = ctx.Saga.CorrelationId,
                            ilosc = ctx.Message.ilosc,
                        };
                    })
                    .Schedule(
                        TO,
                        ctx => new Wiadomosci.Timeout()
                        {
                            CorrelationId = ctx.Instance.CorrelationId,
                        }
                    )
                    .TransitionTo(Niepotwierdzone)
            );

            During(
                Niepotwierdzone,
                When(PotwierdzenieKlienta)
                    .Then(ctx =>
                    {
                        Console.WriteLine("Uzyskano potwierdzenie od klienta!");
                    })
                    .Unschedule(TO)
                    .TransitionTo(PotwierdzoneKlient),
                When(BrakPotwierdzeniaKlienta)
                    .Then(ctx =>
                    {
                        Console.WriteLine("Klient nie potwierdzil zamowienia!");
                    })
                    .Respond(ctx =>
                    {
                        return new Wiadomosci.OdrzucenieZamowienia
                        {
                            CorrelationId = ctx.Saga.CorrelationId,
                            ilosc = ctx.Saga.ilosc,
                            login = ctx.Saga.login,
                        };
                    })
                    .Finalize(),
                When(OdpowiedzWolne)
                    .Then(ctx =>
                    {
                        Console.WriteLine("Uzyskano potwierdzenie od magazynu!");
                    })
                    .TransitionTo(PotwierdzoneMagazyn),
                When(OdpowiedzWolneNegatywna)
                    .Then(ctx =>
                    {
                        Console.WriteLine("Odrzucono zamowienie przez magazyn!");
                    })
                    .Respond(ctx =>
                    {
                        return new Wiadomosci.OdrzucenieZamowienia
                        {
                            CorrelationId = ctx.Saga.CorrelationId,
                            ilosc = ctx.Saga.ilosc,
                            login = ctx.Saga.login,
                        };
                    })
                    .Finalize(),
                //When(TimeoutEvt)
                When(TO.Received)
                    .Then(ctx =>
                    {
                        Console.WriteLine("Timeout zamowienia!");
                    })
                    .Respond(ctx =>
                    {
                        return new Wiadomosci.OdrzucenieZamowienia
                        {
                            CorrelationId = ctx.Saga.CorrelationId,
                            ilosc = ctx.Saga.ilosc,
                            login = ctx.Saga.login,
                        };
                    })
                    .Finalize()
            );

            During(
                PotwierdzoneMagazyn,
                When(PotwierdzenieKlienta)
                    .Then(ctx =>
                    {
                        Console.WriteLine("Uzyskano potwierdzenie od klienta!");
                        Console.WriteLine($"Zamowienie dla {ctx.Saga.login} zaakceptowane!");
                    })
                    .Respond(ctx =>
                    {
                        return new Wiadomosci.AkceptacjaZamowienia
                        {
                            CorrelationId = ctx.Saga.CorrelationId,
                            ilosc = ctx.Saga.ilosc,
                            login = ctx.Saga.login,
                        };
                    })
                    .Unschedule(TO)
                    .Finalize(),
                When(BrakPotwierdzeniaKlienta)
                    .Then(ctx =>
                    {
                        Console.WriteLine("Klient nie potwierdzil zamowienia!");
                    })
                    .Respond(ctx =>
                    {
                        return new Wiadomosci.OdrzucenieZamowienia
                        {
                            CorrelationId = ctx.Saga.CorrelationId,
                            ilosc = ctx.Saga.ilosc,
                            login = ctx.Saga.login,
                        };
                    })
                    .Finalize(),
                //When(TimeoutEvt)
                When(TO.Received)
                    .Then(ctx =>
                    {
                        Console.WriteLine("Timeout zamowienia!");
                    })
                    .Respond(ctx =>
                    {
                        return new Wiadomosci.OdrzucenieZamowienia
                        {
                            CorrelationId = ctx.Saga.CorrelationId,
                            ilosc = ctx.Saga.ilosc,
                            login = ctx.Saga.login,
                        };
                    })
                    .Finalize()
            );

            During(
                PotwierdzoneKlient,
                When(OdpowiedzWolne)
                    .Then(ctx =>
                    {
                        Console.WriteLine("Uzyskano potwierdzenie od magazynu!");
                        Console.WriteLine($"Zamowienie dla {ctx.Saga.login} zaakceptowane!");
                    })
                    .Respond(ctx =>
                    {
                        return new Wiadomosci.AkceptacjaZamowienia
                        {
                            CorrelationId = ctx.Saga.CorrelationId,
                            ilosc = ctx.Saga.ilosc,
                            login = ctx.Saga.login,
                        };
                    })
                    .Finalize(),
                When(OdpowiedzWolneNegatywna)
                    .Then(ctx =>
                    {
                        Console.WriteLine("Odrzucono zamowienie przez magazyn!");
                    })
                    .Respond(ctx =>
                    {
                        return new Wiadomosci.OdrzucenieZamowienia
                        {
                            CorrelationId = ctx.Saga.CorrelationId,
                            ilosc = ctx.Saga.ilosc,
                            login = ctx.Saga.login,
                        };
                    })
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            var repo = new InMemorySagaRepository<ZamowienieDane>();
            var machine = new ZamowienieSaga();
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
                    "saga-sklep",
                    ep =>
                    {
                        ep.StateMachineSaga(machine, repo);
                    }
                );
                sbc.UseInMemoryScheduler();
            });

            bus.Start();
            Console.WriteLine("Sklep wystartowal");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
