using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetLibrary
{
    public class ResultData<T>
    {
        public int Code { get; set; }

        private string _message;
        public string Message
        {
            get {
                if (string.IsNullOrEmpty(_message) && this.Code < 0)
                {
                    if (this.Code == -1500)
                    {
                        return "Object reference not set to an instance of an object.";
                    }

                    return "An error has occurred. Please contact your system administrator.";
                }

                return _message;
            }

            set {
                _message = value;
            }
        }

        private string _error;
        public string Error {
            get {
                return _error ?? _message;
            }

            set {
                _error = value;
            }
        }
        public T Data { get; set; }

        public ResultData()
        { }

        public ResultData(Exception ex)
        {
            Code = int.MinValue;
            Message = ex.Message;
            Error = ex.ToString();
        }

        public override string ToString()
        {
            return string.Format("Code: {0} - Message: {1} - Error: {2}", 
                Code,
                Message ?? "null",
                Error ?? "null");
        }
    }
}