using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using KSR_WCF2;
using Lab5_Zad2;
using Lab5_Zad2.ServiceReference1;

namespace Lab5_Zad2
{
    class Handler : ServiceReference1.IZadanie2Callback
    {
        public IAsyncResult BeginZadanie(string zadanie, int pkt, bool zaliczone, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndZadanie(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void Zadanie([MessageParameter(Name = "zadanie")] string zadanie1, int pkt, bool zaliczone)
        {
            throw new NotImplementedException();
        }
    }
}

    internal class Program
    {
        static void Main(string[] args)
        {
        var c = new Lab5_Zad2.ServiceReference1.Zadanie2Client(new InstanceContext(new
            Handler()));

        c.PodajZadania();
        }
}
