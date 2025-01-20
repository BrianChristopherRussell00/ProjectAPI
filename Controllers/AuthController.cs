using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Models.DTOs;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManger;

        public AuthController(UserManager<IdentityUser> userManger)
        {
            this.userManger = userManger;
        }

        //POST: /api/Auth/Register      
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };



            var identityResult = await userManger.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {

                //Add roles to this User
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {

                    identityResult = await userManger.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                }
                if (identityResult.Succeeded)
                {
                    return Ok("User was registered! Please Login.");
                }
            }
                return BadRequest("Something went wrong");

            }

        }
    }
