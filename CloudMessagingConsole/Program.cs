using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CloudMessagingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var applicationID = "XXXXXXXXXX";

                var senderId = "XXXXXXXXXXX";

                string deviceId = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");

                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data = new

                {

                    to = deviceId,

                    notification = new

                    {

                        body = "Body message here",

                        title = "Title",

                        url = "http://localhost:3034"

                    }
                };

                var serializer = new JavaScriptSerializer();;

                var json =  serializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                tRequest.Headers.Add($"Authorization: key={applicationID}");

                tRequest.Headers.Add($"Sender: id={senderId}");

                tRequest.ContentLength = byteArray.Length;


                using (Stream dataStream = tRequest.GetRequestStream())
                {

                    dataStream.Write(byteArray, 0, byteArray.Length);


                    using (WebResponse tResponse = tRequest.GetResponse())
                    {

                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {

                            using (StreamReader tReader = new StreamReader(dataStreamResponse ?? throw new InvalidOperationException()))
                            {

                                String sResponseFromServer = tReader.ReadToEnd();

                                string str = sResponseFromServer;

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                string str = ex.Message;
                System.Diagnostics.Debug.Write(ex);
            }

        }

    }
}