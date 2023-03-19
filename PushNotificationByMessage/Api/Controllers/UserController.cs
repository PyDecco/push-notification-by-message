using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PushNotificationByMessage.Api.Dtos;
using PushNotificationByMessage.Core.Entities;

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
        public List<User> Post()
        {
            var user = new User { Email = "decola@decola.com", Name = "Decola" };

            var userList = new List<User>();

            userList.Add(user);

            return userList;
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