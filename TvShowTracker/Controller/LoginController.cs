using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvShowTracker.Interfaces;
using TvShowTracker.Model;
namespace TvShowTracker.Controller
{
    [Route("v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly ILoginServices _login;
        public LoginController(ILoginServices login)
        {
            _login = login;
        }
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login(UserPostDTO userLogin)
        {
            if (userLogin == null) return BadRequest();
            if (string.IsNullOrWhiteSpace(userLogin.Email)) return BadRequest();
            if (string.IsNullOrWhiteSpace(userLogin.Password)) return BadRequest();
            try
            {
                string token = _login.Login(userLogin);
                if (!string.IsNullOrEmpty(token))
                    return Ok(token);
                return
                    NotFound();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}