namespace WebApplication1.DTO
{
    public class Response<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
