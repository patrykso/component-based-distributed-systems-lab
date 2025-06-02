using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);
        private string connectionString = "UseDevelopmentStorage=true";
        private BlobContainerClient bcontainer;
        private BlobContainerClient ebcontainer;
        private QueueClient queueClient;
        private Random _rnd = new Random();

        public override void Run()
        {
            Trace.TraceInformation("WorkerRole1 is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");

                try
                {

                    var message = await queueClient.ReceiveMessageAsync(visibilityTimeout: TimeSpan.FromSeconds(5));
                    var messageValue = message.Value;


                    // try
                    // {
                        if (messageValue != null)
                        {
                            var blobName = messageValue.MessageText;

                            var blob = bcontainer.GetBlobClient(blobName);
                            var blobContent = (await blob.DownloadContentAsync()).Value.Content.ToString();


                            if (_rnd.Next(3) == 0)
                            {
                                throw new Exception("Exception");
                            }
                            else
                            {
                                var encoded = Rot13(blobContent);

                                var blobEncoded = ebcontainer.GetBlobClient(blobName);
                                await blobEncoded.UploadAsync(BinaryData.FromString(encoded), overwrite: true);


                                queueClient.DeleteMessage(messageValue.MessageId, messageValue.PopReceipt);
                                Thread.Sleep(1000);
                            }

                        }
                        else
                        {
                            Console.WriteLine("MessageValue null");
                        }

                    // }
                    // catch
                    // {
                    //     Console.WriteLine("Error");
                    //     Thread.Sleep(1000);

                    // }
                } catch
                {
                    Console.WriteLine("Catch");
                    Thread.Sleep(1000);

                }
            }
        }

        public static string Rot13(string value)
        {
            char[] array = value.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                int number = (int)array[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                array[i] = (char)number;
            }
            return new string(array);
        }

        public override bool OnStart()
        {
            // Use TLS 1.2 for Service Bus connections
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("WorkerRole1 has been started");

            BlobClientOptions options = new BlobClientOptions(BlobClientOptions.ServiceVersion.V2021_06_08);

            var options2 = new QueueClientOptions(QueueClientOptions.ServiceVersion.V2021_06_08);

            var bsc = new BlobServiceClient(connectionString, options);
            bcontainer = bsc.GetBlobContainerClient("blobs-files");
            ebcontainer = bsc.GetBlobContainerClient("blob-encoded-files");
            bcontainer.CreateIfNotExistsAsync();
            ebcontainer.CreateIfNotExistsAsync();

            queueClient = new QueueClient(connectionString, "queue-to-encode", options2);
            queueClient.CreateIfNotExists();

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole1 is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRole1 has stopped");
        }


    }
}
