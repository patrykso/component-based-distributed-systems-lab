namespace Komunikaty
{
    public interface IPubl
    {
        string tekst1 { get; set; }
        int numer_wiadomosci {  get; set; }
    }
    public class Publ : IPubl
    {
        public string tekst1 { get; set; }
        public int numer_wiadomosci { get; set; }

    }

    public interface IUstaw
    {
        bool dziala { get; set; }
    }

    public class Ustaw : IUstaw
    {
        public bool dziala { get; set; }
    }

    public interface IOdpA
    {
        string kto {  get; set; }
    }

    public class OdpA : IOdpA
    {
        public string kto { get; set; }
    }

    public interface IOdpB
    {
        string kto { get; set; }
    }

    public class OdpB : IOdpB
    {
        public string kto { get; set; }
    }
}
