namespace Core.Models.Response
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null!)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<Error> Errors { get; set; }
        public T Data { get; set; }
    }

    public class Error
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
    }
}
