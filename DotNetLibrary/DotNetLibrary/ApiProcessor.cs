using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using DotNetLibrary.Interfaces;

namespace DotNetLibrary
{
    public class ApiProcessor : IApiProcessor
    {
        public ApiProcessor()
        { }

        public T Delete<T>(string url, object obj, int timeout = 60)
        {
            return SendRequest<T>(HttpMethod.Delete, url, obj, timeout);
        }

        public async Task<T> DeleteAsync<T>(string url, object obj, int timeout = 60)
        {
            HttpClient client = new HttpClient();
            Task<T> task = new Task<T>(() => Delete<T>(url, obj, timeout));
            task.Start();

            return await task;
        }

        public T Get<T>(string url, int timeout = 60)
        {
            return SendRequest<T>(HttpMethod.Get, url, null, timeout);
        }

        public async Task<T> GetAsync<T>(string url, int timeout = 60)
        {
            Task<T> task = new Task<T>(() => Get<T>(url, timeout));
            task.Start();

            return await task;
        }

        public T Post<T>(string url, object obj, int timeout = 60)
        {
            return SendRequest<T>(HttpMethod.Post, url, obj, timeout);
        }

        public async Task<T> PostAsync<T>(string url, object obj, int timeout = 60)
        {
            Task<T> task = new Task<T>(() => Post<T>(url, obj, timeout));
            task.Start();

            return await task;
        }

        public T Put<T>(string url, object obj, int timeout = 60)
        {
            return SendRequest<T>(HttpMethod.Put, url, obj, timeout);
        }

        public async Task<T> PutAsync<T>(string url, object obj, int timeout = 60)
        {
            Task<T> task = new Task<T>(() => Put<T>(url, obj, timeout));
            task.Start();

            return await task;
        }

        private T SendRequest<T>(HttpMethod method, string url, object obj, int timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method.ToString();
            request.ContentType = "application/json";
            request.Timeout = timeout * 1000;

            if (method != HttpMethod.Get && obj != null)
            {
                string json = JsonConvert.SerializeObject(obj);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;

                Stream stream = request.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string str = reader.ReadToEnd();
            reader.Close();
            response.Close();

            T rs = JsonConvert.DeserializeObject<T>(str);

            return rs;
        }
    }
}
