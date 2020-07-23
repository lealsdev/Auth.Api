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
        private readonly IMapper _mapper;
        public AuthController(IUserApplication userApplication, IMapper mapper)
        {
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
    }
}