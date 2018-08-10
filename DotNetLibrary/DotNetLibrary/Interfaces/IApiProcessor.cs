using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLibrary.Interfaces
{
    public interface IApiProcessor
    {
        T Delete<T>(string url, object obj, int timeout = 60);
        Task<T> DeleteAsync<T>(string url, object obj, int timeout = 60);
        T Get<T>(string url, int timeout = 60);
        Task<T> GetAsync<T>(string url, int timeout = 60);
        T Post<T>(string url, object obj, int timeout = 60);
        Task<T> PostAsync<T>(string url, object obj, int timeout = 60);
        T Put<T>(string url, object obj, int timeout = 60);
        Task<T> PutAsync<T>(string url, object obj, int timeout = 60);
    }
}
