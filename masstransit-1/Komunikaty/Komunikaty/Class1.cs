namespace Komunikaty
{
    public interface IKomunikat
    {
        string tekst { get; set; }
    }

    public class Komunikat : IKomunikat
    {
        public string tekst { get; set; }
    }

    public interface IKomunikat2
    {
        string tekst2 { get; set; }
    }

    public class Komunikat2 : IKomunikat2
    {
        public string tekst2 { get; set;  }
    }

    public interface IKomunikat3 : IKomunikat2, IKomunikat
    {
        string tekst3 { get; set; }
    }

    public class Komunikat3 : IKomunikat3
    {
        public string tekst3 { get; set; }
        public string tekst2 { get; set; }
        public string tekst { get; set; }
    }
}
