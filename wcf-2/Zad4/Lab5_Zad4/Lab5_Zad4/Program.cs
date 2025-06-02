using KSR_WCF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_Zad4
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Zadanie4Service : IZadanie4
    {
        private int licznik;
        public int Dodaj(int v)
        {
            licznik += v;
            return licznik;
        }

        public void Ustaw(int v)
        {
            licznik = v;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Zadanie4Service));

            host.AddServiceEndpoint(typeof(IZadanie4), new
                NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf2-zad4");

            host.Open();
            Console.WriteLine("Serwis");
            Console.ReadKey();
            host.Close();
        }
    }
}
