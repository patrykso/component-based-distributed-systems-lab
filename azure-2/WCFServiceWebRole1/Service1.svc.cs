using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private string connectionString = "UseDevelopmentStorage=true";
        private BlobContainerClient bcontainer;
        private BlobContainerClient ebcontainer;
        private QueueClient queueClient;



        public Service1()
        {
            BlobClientOptions options = new BlobClientOptions(BlobClientOptions.ServiceVersion.V2021_06_08);
            var options2 = new QueueClientOptions(QueueClientOptions.ServiceVersion.V2021_06_08);
            var bsc = new BlobServiceClient(connectionString, options);
            bcontainer = bsc.GetBlobContainerClient("blobs-files");
            ebcontainer = bsc.GetBlobContainerClient("blob-encoded-files");
            bcontainer.CreateIfNotExistsAsync();
            ebcontainer.CreateIfNotExistsAsync();

            queueClient = new QueueClient(connectionString, "queue-to-encode", options2);
            queueClient.CreateIfNotExists();
        }

        public void Koduj(string nazwa, string tresc)
        {
            if (nazwa == null || tresc == null)
            {
                return;
            } else
            {
                var blob = bcontainer.GetBlobClient($"{nazwa}");
                try
                {
                    blob.Upload(BinaryData.FromString(tresc), overwrite: true);
                    var encodedBlob = Convert.ToBase64String(Encoding.UTF8.GetBytes(nazwa));
                    queueClient.SendMessage(nazwa);
                    return;
                } catch
                {
                    Console.WriteLine("Catch");
                }
            }
        }
        public string Pobierz(string nazwa)
        {
            if (nazwa != null)
            {
                var blob = ebcontainer.GetBlobClient($"{nazwa}");
                try
                {
                    var downloaded = blob.DownloadContent();
                    return downloaded.Value.Content.ToString();
                } catch
                {
                    Console.WriteLine("Catch");
                }
            }
            return "Error";
        }
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
