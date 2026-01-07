using DWDWalks.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DWDWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly UserManager<IdentityUser> userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.UserName
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerDto.Roles != null && registerDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User is registred successfully! Please login.");
                    }
                }
            }

            return BadRequest("User registration failed!");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginDto.Password);

                if (checkPasswordResult)
                {
                    // Create a token
                    return Ok();
                }
            }
            return BadRequest("Invalid username or password!");

            

        }
    }
}
