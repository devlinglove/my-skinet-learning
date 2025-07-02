namespace Webapi.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public string? Details { get; set; }

        public ApiErrorResponse(int stCode, string msg, string? details)
        {
            StatusCode = stCode;
            Message = msg;
            Details = details;
        }
        
    }
}
