using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR_WCF1;
using System.ServiceModel;
using System.ServiceModel.Description;
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

    public class Zadanie7 : IZadanie7
    {
        public void RzucWyjatek7(string A, int B)
        {
            Console.WriteLine("Wyjatek7");
            throw new FaultException<Wyjatek7>(new Wyjatek7()
            {
                a = A,
                b = B,
                opis = "Opis Wyjatek 7"
            }, "Powod Wyjatek7");
        }
    }
    internal class Program
    {


        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Zadanie7));

            host.AddServiceEndpoint(typeof(IZadanie7), new
                NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf1-zad7");

            //zad 4
            host.AddServiceEndpoint(typeof(IZadanie7), new
                NetTcpBinding(),
                "net.tcp://127.0.0.1:55765");

            // zad3
            var b = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (b == null) b = new ServiceMetadataBehavior();
            host.Description.Behaviors.Add(b);

            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                "net.pipe://localhost/metadane");

            host.Open();
            Console.ReadKey();
            host.Close();
        }
    }
}
