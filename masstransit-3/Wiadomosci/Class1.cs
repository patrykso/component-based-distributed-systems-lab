﻿using MassTransit;

namespace Wiadomosci
{
    public interface IStartZamowienia // klient -> sklep
    {
        int ilosc { get; set; }
        string login { get; set; }
    }

    public class StartZamowienia : IStartZamowienia
    {
        public int ilosc { get; set; }
        public string login { get; set; }
    }

    public interface IPytanieoPotwierdzenie : CorrelatedBy<Guid> // sklep -> klient
    {
        int ilosc { get; set; }
        string login { get; set; }
    }

    public class PytanieoPotwierdzenie : IPytanieoPotwierdzenie
    {
        public Guid CorrelationId { get; set; }
        public int ilosc { get; set; }
        public string login { get; set; }
    }

    public interface IPotwierdzenie : CorrelatedBy<Guid> // klient -> sklep
    {
        // brak pola?
    }

    public class Potwierdzenie : IPotwierdzenie
    {
        public Guid CorrelationId { get; set; }
    }

    public interface IBrakPotwierdzenia : CorrelatedBy<Guid> // klient -> sklep
    {
        // brak pola?
    }

    public class BrakPotwierdzenia : IBrakPotwierdzenia
    {
        public Guid CorrelationId { get; set; }
    }

    public interface IPytanieoWolne : CorrelatedBy<Guid> // sklep -> magazyn
    {
        int ilosc { get; set; }
    }

    public class PytanieoWolne : IPytanieoWolne
    {
        public Guid CorrelationId { get; set; }
        public int ilosc { get; set; }
    }

    public interface IOdpowiedzWolne : CorrelatedBy<Guid> // magazyn -> sklep
    {
        // brak pola
    }

    public class OdpowiedzWolne : IOdpowiedzWolne
    {
        public Guid CorrelationId { get; set; }
    }

    public interface IOdpowiedzWolneNegatywna : CorrelatedBy<Guid> // magazyn -> sklep
    {
        // brak pola
    }

    public class OdpowiedzWolneNegatywna : IOdpowiedzWolneNegatywna
    {
        public Guid CorrelationId { get; set; }
    }

    public interface IAkceptacjaZamowienia : CorrelatedBy<Guid> // sklep -> klient
    {
        int ilosc { get; set; }
        string login { get; set; }
    }

    public class AkceptacjaZamowienia : IAkceptacjaZamowienia
    {
        public Guid CorrelationId { get; set; }
        public int ilosc { get; set; }
        public string login { get; set; }
    }

    public interface IOdrzucenieZamowienia : CorrelatedBy<Guid> // sklep -> klient
    {
        int ilosc { get; set; }
        string login { get; set; }
    }

    public class OdrzucenieZamowienia : IOdrzucenieZamowienia
    {
        public Guid CorrelationId { get; set; }
        public int ilosc { get; set; }
        public string login { get; set; }
    }

    public interface ITimeout : CorrelatedBy<Guid>
    {
        Guid CorrelationId { get; set; }
    }

    public class Timeout : ITimeout
    {
        public Guid CorrelationId { get; set; }
    }
}
