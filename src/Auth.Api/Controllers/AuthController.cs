using System.Threading.Tasks;
using Auth.Api.Dto;
using Auth.Application.Interfaces;
using Auth.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly IAuthApplication _authApplication;
        private readonly IMapper _mapper;
        public AuthController(
            IUserApplication userApplication,        
            IAuthApplication authApplication,
            IMapper mapper)
        {
            this._authApplication = authApplication;
            this._mapper = mapper;
            this._userApplication = userApplication;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister)
        {
            if (await this._userApplication.checkUserExistsBy(userForRegister.Email))
            {
                var userToCreate = _mapper.Map<User>(userForRegister);

                var createdUser = await this._userApplication.Add(userToCreate);

                var userToReturn = _mapper.Map<UserForDetailed>(createdUser);

                return CreatedAtRoute("GetBy", new { controller = "User", id = createdUser.Id }, userToReturn);
            }
            else
            {
                return BadRequest($"The email {userForRegister.Email} was already registered.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLogin userForLogin)
        {
            var userFromRepo = await this._authApplication.Login(
                userForLogin.Email.ToLower(), userForLogin.Password);

            if (userFromRepo == null)
                return Unauthorized();

            return Ok();
        }
    }
}