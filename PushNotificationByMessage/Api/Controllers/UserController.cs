using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PushNotificationByMessage.Api.Dtos;
using PushNotificationByMessage.Core.Entities;
using PushNotificationByMessage.Core.Request;

namespace PushNotificationByMessage.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IGenericRepository<User> _usersRepo;
        private readonly IMapper _mapper;

        public UserController(IGenericRepository<User> userRepo, IMapper mapper, IAccountRepository accountRepository)
        {
            _usersRepo = userRepo;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<int>> PostUser([FromBody] UserRegisterRequest userRequest)
        {
            try { 
            var result = await _accountRepository.SignUpAsync(userRequest);
            
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return Unauthorized();
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginToReturnDto>> PostLogin([FromBody] LoginRequest loginRequest)
        {
            var user = await _usersRepo.GetByEmaildAsync(loginRequest.Login);

            if (user == null) return NotFound(new ApiResponse(404));

            var result = await _accountRepository.LoginAsync(loginRequest);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(new LoginToReturnDto()
            {
                Token = result,
                User = new UserLogin() { id = user.Id, name = user.Name, email = user.Email, }
            });

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserToReturnDto>> GetUser(int id)
        {
            var user = await _usersRepo.GetByIdAsync(id);

            if (user == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<User, UserToReturnDto>(user);
        }

    }
}