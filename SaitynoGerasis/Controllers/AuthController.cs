using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SaitynoGerasis.Auth;
using SaitynoGerasis.Auth.Model;
using SaitynoGerasis.Data.Entities;

namespace SaitynoGerasis.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class AuthController :ControllerBase
    {
        private readonly UserManager<naudotojas> _userManager;
        private readonly IJwtTokenService1 _jwtTokenService1;
        public AuthController(UserManager<naudotojas> userManager, IJwtTokenService1 jwtTokenService1)
        {
            _userManager = userManager;
            _jwtTokenService1 = jwtTokenService1;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserDto register)
        {
            var user = await _userManager.FindByNameAsync(register.UserName);
            if (user !=null)
            {
                return BadRequest("Request invaliddd");
            }
            //public string Vardas { get; set; }
            //public string Pavarde { get; set; }
            //public bool Pardavejas { get; set; }
            //public bool Pirkejas { get; set; }
            //public string Slaptazodis { get; set; }
            var newUser = new naudotojas
            {
                UserName = register.UserName,
                Email = register.Email
            };
            var CreateUser = await _userManager.CreateAsync(newUser, register.Password);
            if (!CreateUser.Succeeded)
            {
                return BadRequest("Could not create a user");
            }
            await _userManager.AddToRoleAsync(newUser, Roles.User);
            return CreatedAtAction(nameof(Register), new UserDto(newUser.Id, newUser.UserName, newUser.Email));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginDto login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                return BadRequest("User Name or password is invalid");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, login.Password);

            if (!isPasswordValid)
            {
                return BadRequest("User name or password is invalid");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtTokenService1.CreateAccessToken(user.UserName, user.Id, roles);

            return Ok(new Succes(accessToken));
        }
    }
}
