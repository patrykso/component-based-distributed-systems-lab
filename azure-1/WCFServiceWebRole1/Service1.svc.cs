using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Blob;
using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private TableServiceClient sc1;
        private string sc2;

        //private CloudBlobClient bclient;
        //private CloudBlobContainer bcontainer;
        private TableClient users;
        private TableClient sessions;
        private BlobContainerClient bcontainer;
        private BlobServiceClient blobService;

        public Service1()
        {
            sc1 = new TableServiceClient("UseDevelopmentStorage=true");
            //sc2 = new TableServiceClient("UseDevelopmentStorage=true");
            sc2 = "UseDevelopmentStorage=true";



            users = sc1.GetTableClient("users");
            users.CreateIfNotExists();

            sessions = sc1.GetTableClient("sessions");
            sessions.CreateIfNotExists();

            //CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
            //CloudBlobClient client = account.CreateBlobClient();
            //bclient = account.CreateCloudBlobClient();
            //bcontainer = bclient.GetContainerReference("blob");
            //bcontainer.CreateIfNotExists();

            var blobOptions = new BlobClientOptions(BlobClientOptions.ServiceVersion.V2021_04_10);
            blobService = new BlobServiceClient(sc2, blobOptions);
            bcontainer = blobService.GetBlobContainerClient("files");
            blobService.DeleteBlobContainer("userfiles");
            blobService.CreateBlobContainer("userfiles");
            try
            {
                bcontainer.CreateIfNotExists();
            } catch { };
        }

        public bool Create(string login, string haslo)
        {
            if (login == null || haslo == null)
            {
                return false;
            }
            else
            {
                var e = new User(login, haslo);
                users.AddEntity(e);
                return true;
            }
        }

        public string Login(string login, string haslo)
        {
            if (login == null || haslo == null)
            {
                return "Pusty login i/lub haslo!";
            } else
            {
                Response<User> r = users.GetEntity<User>("users", login);
                User e = r.Value;

                if (e == null || e.haslo != haslo)
                {
                    return "Nieprawidlowe dane";
                } else
                {
                    string sessionId = Guid.NewGuid().ToString();
                    e.sessionId = sessionId;
                    users.UpdateEntity(e, e.ETag, TableUpdateMode.Replace);
                    //var se = new Session(login, sessionId);
                    //sessions.AddEntity(se);
                    return e.sessionId;
                }
            }
        }

        public bool Logout(string login)
        {
            if (login == null)
            {
                return false;
            } else
            {
                Response<User> r = users.GetEntity<User>("users", login);
                User e = r.Value;

                e.sessionId = null;
                users.UpdateEntity(e, e.ETag, TableUpdateMode.Replace);
                return true;
            }
        }

        public bool Put(string nazwa, string tresc, string id_sesji)
        {
            if (nazwa == null || tresc == null || id_sesji == null) { 
                return false;
            } else
            {
                var user = users.Query<User>(u => u.PartitionKey == "users" && u.sessionId == id_sesji).FirstOrDefault();
                if (user == null)
                {
                    return false;
                } else
                {
                    //string blobName = $"{user.RowKey}/{nazwa}.txt";
                    //var blob = bcontainer.GetBlockBlobReference(blobName);

                    //var bytes = new ASCIIEncoding().GetBytes(tresc);
                    //var s = new System.IO.MemoryStream(bytes);
                    //blob.UploadFromStream(s);
                    //return true;

                    var blob = bcontainer.GetBlobClient($"{user.RowKey}/{nazwa}");
                    try
                    {
                        blob.Upload(BinaryData.FromString(tresc), overwrite: true);
                        return true;
                    } catch { return false; }
                }
            }
        }

        public string Get(string nazwa, string id_sesji)
        {
            if (nazwa == null || id_sesji == null)
            {
                return "Nieprawidlowe dane";
            }
            else
            {
                var user = users.Query<User>(u => u.PartitionKey == "users" && u.sessionId == id_sesji).FirstOrDefault();
                if (user == null)
                {
                    return "Nieprawidlowe dane 2";
                }
                else
                {
                    var blob = bcontainer.GetBlobClient($"{user.RowKey}/{nazwa}");
                    try
                    {
                        var downloaded = blob.DownloadContent();
                        //return dwonloaded.Value.Content.ToString();
                        return downloaded.Value.Content.ToString();
                    } catch
                    {
                        return null;
                    }
                    //string blobName = $"{user.RowKey}/{nazwa}.txt";
                    //var blob = bcontainer.GetBlockBlobReference(blobName);

                    //if (!blob.Exists())
                    //    return null;

                    //var s2 = new System.IO.MemoryStream();
                    //blob.DownloadToStream(s2);
                    //string content = Encoding.UTF8.GetString(s2.ToArray());

                    //return content;
                }
            }
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
