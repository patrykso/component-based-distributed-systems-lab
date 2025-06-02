using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using KSR_WCF1;
using System.Runtime.Serialization;

namespace Lab4_Zad7
{
    [ServiceContract]
    public interface IZadanie7
    {
        [OperationContract]
        [FaultContract(typeof(Wyjatek7))]
        void RzucWyjatek7(string a, int b);
    }

    [DataContract]
    public class Wyjatek7
    {
        [DataMember]
        public string opis { get; set; }
        [DataMember]
        public string a { get; set; }
        [DataMember]
        public int b { get; set; }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var fact = new ChannelFactory<IZadanie7>(new
                    NetNamedPipeBinding(),
                 new EndpointAddress("net.pipe://localhost/ksr-wcf1-zad7"));

            var client = fact.CreateChannel();

            try
            {
                client.RzucWyjatek7("wyjatek7", 7);
            } catch (FaultException<Wyjatek7> e)
            {
                Console.WriteLine("Zlapano wyjatek");
                Console.WriteLine(e.Detail.opis);
                Console.WriteLine(e.Detail.a);
                Console.WriteLine(e.Detail.b);
            }

            ((IDisposable)client).Dispose();
            fact.Close();
        }
    }
}
