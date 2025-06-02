using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using KSR_WCF2;

namespace Lab5_Zad3
{
    public class Zadanie3Service : IZadanie3
    {
        public void TestujZwrotny()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IZadanie3Zwrotny>();

            for (int i = 0; i <= 30; i++) {
                callback.WolanieZwrotne(i, (i * i * i) - (i * i));
                }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Zadanie3Service));

            host.AddServiceEndpoint(typeof(IZadanie3), new
                NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf2-zad3");

            host.Open();
            Console.WriteLine("Serwis");
            Console.ReadKey();
            host.Close();
        }
    }
}
