namespace Catalog.Application.Common.Models
{
    public class RestException
    {
        public RestException(int statusCode, string message, string details = default!)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}