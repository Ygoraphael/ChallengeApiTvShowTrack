using Microsoft.AspNetCore.Mvc;
using TvShowTracker.Interfaces;
using TvShowTracker.Model;

namespace TvShowTracker.Controller
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _repUser;
        public UsersController(IUserServices repUser)
        {
            _repUser = repUser;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                var user = await _repUser.GetUser(id);
                if (user == null)
                    return NotFound();
                else
                    return Ok(user);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostUser(UserPostDTO user)
        {
            if (user == null) return BadRequest();
            if (string.IsNullOrWhiteSpace(user.Email)) return BadRequest();
            if (string.IsNullOrWhiteSpace(user.Password)) return BadRequest();
            try
            {
                var newUser = await _repUser.CreateUser(user);
                if (newUser == null)
                    return NotFound();
                else
                    return Created("GetUser", newUser); 
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}