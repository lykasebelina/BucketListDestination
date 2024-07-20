using BucketListModels;
using BucketListServices;
using Microsoft.AspNetCore.Mvc;

namespace BucketList.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserGetServices _userGetServices;

        public UserController()
        {
            _userGetServices = new UserGetServices();
        }

        [HttpPost("validate")]
        public IActionResult ValidateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("User cannot be null");

            bool isValid = _userGetServices.ValidateUser(user.Username, user.Password);
            if (isValid)
                return Ok();

            return Unauthorized();
        }
    }
}
 