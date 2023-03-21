using AutoMapper;
using Core.Interfaces;
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
        private readonly IGenericRepository<User> _usersRepo;
        private readonly IMapper _mapper;

        public UserController(IGenericRepository<User> userRepo, IMapper mapper)
        {
            _usersRepo = userRepo;
            _mapper = mapper; 
        }

        [HttpPost("register")]
        public async Task<ActionResult<int>> PostUser([FromBody] UserRequest userRequest)
        {
            var user = new User
            {
                Id = userRequest.Id,
                Name = userRequest.Name,
                Email = userRequest.Email,
                Address = userRequest.Address,
                NameCompany = userRequest.NameCompany,
                Password = userRequest.Password,
                Telephone = userRequest.Telephone,
            }; // Não consegui usar auto mapper nessa circustancias
            var result = await _usersRepo.PostAsync(user);

            return CreatedAtAction(nameof(GetUser), new { id = result }, result); //não consegui colocar mensagem no retorno
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