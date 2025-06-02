using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR_WCF2;

namespace Lab5_Zad1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new ServiceReference1.Zadanie1Client();

            IAsyncResult res = client.BeginDlugieObliczenia(null, null);

            for(int i = 0; i <= 20; i++)
            {
                client.Szybciej(i, (3 * i * i - 2 * i));

            }

            string wynik = client.EndDlugieObliczenia(res);
            Console.WriteLine(wynik);
        }
    }
}
