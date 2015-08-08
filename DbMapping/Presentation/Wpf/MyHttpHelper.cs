using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DbMapping
{
    public class MyHttpHelper
    {
        public static string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    var result = new StreamReader(stream).ReadToEnd();
                    return result;
                }
            }
        }

        public static T Get<T>(string url) where T : class
        {
            var str = Get(url);
            var obj = JsonConvert.DeserializeObject<T>(str);
            return obj;
        }

        public static T Post<T>(string url, string jsonContent) where T : class
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    var result = new StreamReader(stream).ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
        }
    }
}
