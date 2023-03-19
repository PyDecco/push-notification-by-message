namespace PushNotificationByMessage.Api.Controllers
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string error = null)
        {
            Error = error ?? GetDefaultMessageStatusCode(statusCode);
        }

        public string Error { get; set; }
        
        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Erros are the path to the dark side. Errors lead to anger. Anger leads to hate." +
                " Hate leads to career change",
                _ => null
            };
        }
    }
}
