namespace Basket.Application.Common.Models
{
    public static class ApiResponse
    {
        public static ApiResponse<T> Fail<T>(string message, T data = default!) => new ApiResponse<T>(data, message, true);
        public static ApiResponse<T> Ok<T>(T data=default!, string message = "Successful") => new ApiResponse<T>(data, message, false);
    }

    public class ApiResponse<T>
    {
        public ApiResponse(T data, string message, bool error)
        {
            Data = data;
            Message = message;
            Error = error;
        }

        public T Data { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
    }
}