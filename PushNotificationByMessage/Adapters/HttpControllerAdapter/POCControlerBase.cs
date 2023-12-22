using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PushNotificationByMessage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class POCControlerBase : ControllerBase
    {
        protected async Task<ObjectResult> EncasuplarException(Exception e, HttpStatusCode statusCode)
        {
            var errorObj = new { error = e.Message };
            var error = new ObjectResult(errorObj)
            {
                StatusCode = (int)statusCode
            };
            return error;
        }
    }
}
