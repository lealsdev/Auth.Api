using System.Threading.Tasks;
using Auth.Api.Dto;
using Auth.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        public AuthController(IUserApplication userApplication)
        {
            this._userApplication = userApplication;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister)
        {
            if(await this._userApplication.checkUserExistsBy(userForRegister.Email))
            {
                /*
                if (await this._repository.UserExists(userForRegisterDto.Username))
                    return BadRequest("Username already exists.");

                var userToCreate = _mapper.Map<User>(userForRegisterDto);

                var createdUser = await this._repository.Register(
                    userToCreate, userForRegisterDto.Password);

                var userToReturn = _mapper.Map<UserForDetailedDto>(createdUser);

                return CreatedAtRoute("GetUser", new { controller="Users", id=createdUser.Id }, userToReturn);
                */
                return Ok(123);
            }
            else
            {
                return BadRequest($"The email {userForRegister.Email} was already registered.");
            }
        }
    }
}