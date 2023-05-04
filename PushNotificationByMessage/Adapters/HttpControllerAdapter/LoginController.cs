using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PushNotificationByMessage.Models.Dtos;
using PushNotificationByMessage.Ports;
using PushNotificationByMessage.Ports.In;
using System.Net;

namespace PushNotificationByMessage.Api.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : POCControlerBase
    {
        private readonly ILoginUseCase _loginUseCase;

        public LoginController( ILoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginToReturnDto>> PostLogin([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _loginUseCase.Login(loginDto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException u)
            {
                return await base.EncasuplarException(u, HttpStatusCode.Unauthorized);
            }
            catch (Exception e) { return StatusCode(500); }
        }
    }
}