using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using KSR_WCF1;

namespace Lab4_Zad1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fact = new ChannelFactory<IZadanie1>(new
                    NetNamedPipeBinding(),
                 new EndpointAddress("net.pipe://localhost/ksr-wcf1-test"));

            var client = fact.CreateChannel();

            string result = client.Test("Zadanie 1");
            Console.WriteLine(result);

            try
            {
                client.RzucWyjatek(true);
            } catch(FaultException<KSR_WCF1.Wyjatek> e)
            {
                Console.WriteLine(e.Detail.opis);
                client.OtoMagia(e.Detail.magia);
            }

            ((IDisposable)client).Dispose();
            fact.Close();
        }
    }
}
