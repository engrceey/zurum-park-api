using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZurumPark.Entities;
using ZurumPark.Repository.IRepository;

namespace ZurumPark.Controllers
{
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User model)
        {
            var user = _userRepository.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new {message =  "Username or password is incorrect"});
            }
            return Ok(user);
        }
    }
}