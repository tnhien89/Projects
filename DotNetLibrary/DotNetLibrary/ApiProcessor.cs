using System;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using NLog;
using Newtonsoft.Json;

namespace DotNetLibrary
{
    public class ApiProcessor
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();

        public static ResultData<T> Delete<T>(string url, object obj, int timeout = 60)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "DELETE";
                request.Timeout = timeout * 1000;
                request.ContentType = "application/json";

                if (obj != null)
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

                ResultData<T> rs = JsonConvert.DeserializeObject<ResultData<T>>(str);
                if (rs.Code < 0)
                {
                    _log.Error(rs.ToString());
                }

                return rs;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return new ResultData<T>(ex);
            }
        }

        public static async Task<ResultData<T>> DeleteAsync<T>(string url, object obj, int timeout = 60)
        {
            HttpClient client = new HttpClient();
            Task<ResultData<T>> task = new Task<ResultData<T>>(() => Delete<T>(url, obj, timeout));
            task.Start();

            return await task;
        }

        public static ResultData<T> Get<T>(string url, int timeout = 60)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Timeout = timeout * 1000;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string str = reader.ReadToEnd();
                reader.Close();
                response.Close();

                ResultData<T> rs = JsonConvert.DeserializeObject<ResultData<T>>(str);
                if (rs.Code < 0)
                {
                    _log.Error(rs.ToString());
                }

                return rs;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return new ResultData<T>(ex);
            }
        }

        public static async Task<ResultData<T>> GetAsync<T>(string url, int timeout = 60)
        {
            Task<ResultData<T>> task = new Task<ResultData<T>>(() => Get<T>(url, timeout));
            task.Start();

            return await task;
        }

        public static ResultData<T> Post<T>(string url, object obj, int timeout = 60)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Timeout = timeout * 1000;

                if (obj != null)
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

                ResultData<T> rs = JsonConvert.DeserializeObject<ResultData<T>>(str);
                if (rs.Code < 0)
                {
                    _log.Error(rs.ToString());
                }

                return rs;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return new ResultData<T>(ex);
            }
        }

        public static async Task<ResultData<T>> PostAsync<T>(string url, object obj, int timeout = 60)
        {
            Task<ResultData<T>> task = new Task<ResultData<T>>(() => Post<T>(url, obj, timeout));
            task.Start();

            return await task;
        }

        public static ResultData<T> Put<T>(string url, object obj, int timeout = 60)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "PUT";
                request.ContentType = "application/json";
                request.Timeout = timeout * 1000;

                if (obj != null)
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

                ResultData<T> rs = JsonConvert.DeserializeObject<ResultData<T>>(str);
                if (rs.Code < 0)
                {
                    _log.Error(rs.ToString());
                }

                return rs;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return new ResultData<T>(ex);
            }
        }

        public static async Task<ResultData<T>> PutAsync<T>(string url, object obj, int timeout = 60)
        {
            Task<ResultData<T>> task = new Task<ResultData<T>>(() => Put<T>(url, obj, timeout));
            task.Start();

            return await task;
        }
    }
}
