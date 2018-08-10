namespace Company.Models
{
    public class ResultDTO<T>
    {
        public int Code { get; set; }

        private string _message;
        public string Message
        {
            get
            {
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

            set
            {
                _message = value;
            }
        }
        public T Data { get; set; }
    }
}
