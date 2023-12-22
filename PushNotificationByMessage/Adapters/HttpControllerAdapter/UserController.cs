using Microsoft.AspNetCore.Mvc;
using PushNotificationByMessage.Models.Dtos;
using PushNotificationByMessage.Ports;
using PushNotificationByMessage.Ports.In;
using System.Net;

namespace PushNotificationByMessage.Api.Controllers
{
    [ApiController]
    [Route("api/user/")]
    public class UserController : POCControlerBase
    {
        private readonly IRegisterUseCase _registerUseCase;
        private readonly ICRUDUserUseCase _crudUserUseCase;

        public UserController(IRegisterUseCase registerUseCase, ICRUDUserUseCase crudUserUseCase)
        {
            _registerUseCase = registerUseCase;
            _crudUserUseCase = crudUserUseCase;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterResponse>> PostUser([FromBody] UserRegisterDto userDto)
        {
            try
            {
                var result = await _registerUseCase.Register(userDto);

                return Ok(result);
            }
            catch (NullReferenceException ex)
            {
                return await EncasuplarException(ex, HttpStatusCode.Forbidden);
            }
            catch (UnauthorizedAccessException ex)
            {
                return await EncasuplarException(ex, HttpStatusCode.Unauthorized);
            }
            catch (Exception ex) 
            {
                return await EncasuplarException(ex, HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetByUserReturnDto>> GetUser(int id)
        {
            try
            {
                var user = await _crudUserUseCase.GetById(id);
                var result  = Ok(user);

                return result;
            }
            catch (KeyNotFoundException k)
            {
                return await EncasuplarException(k, HttpStatusCode.NotFound);

            }
            catch (Exception e)
            {
                return await EncasuplarException(e, HttpStatusCode.InternalServerError);
            }
        }
    }
}
