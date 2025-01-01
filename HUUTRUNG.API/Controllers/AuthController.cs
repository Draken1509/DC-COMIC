using HUUTRUNG.Models.Domain;
using HUUTRUNG.Models.DTO.RequestDTO;
using HUUTRUNG.Models.DTO.ResponseDTO;
using HUUTRUNG_WEBAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HUUTRUNG_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;

        }
		#region khoa tam thoi
		// POST: /api/Auth/Register
		//[HttpPost]
  //      [Route("Register")]
  //      public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDto)
  //      {
  //          var identityUser = new IdentityUser
  //          {
  //              UserName = registerRequestDto.Username,
  //              Email = registerRequestDto.Username
  //          };

  //          var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
  //          if (identityResult.Succeeded)
  //          {
  //              // Add roles to this User
  //              if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
  //              {
  //                  identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

  //                  if (identityResult.Succeeded)
  //                  {
  //                      return Ok("User was registered! Please login.");
  //                  }
  //              }
  //          }

  //          return BadRequest("Something went wrong");
  //      }
		#endregion
		// POST: /api/Auth/Login
		[HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    // Get Roles for this user
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create Token

                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {

                            Email = loginRequestDto.Email,
                            Roles = roles.ToList(),
                            Token = "TOKEN",
                            JwtToken = jwtToken,
                            ApplicationUserId = user.Id
                        };
                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or password incorrect");
        }
    }
}

