using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Api.Dto;
using Auth.Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly IMapper _mapper;
        public UsersController(IUserApplication userApplication, IMapper mapper)
        {
            this._mapper = mapper;
            this._userApplication = userApplication;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get()
        {
            var users = await this._userApplication.Get();
            var usersForList = _mapper.Map<IEnumerable<UserForList>>(users);

            return Ok(usersForList);
        }

        [HttpGet("{id}", Name="GetBy")]
        public async Task<IActionResult> Get(Guid id)
        {
            if(this.isLoggedUserAbleToReadAndUpdateUserData(id) || this.isLoggedUserAdmin())
            {
                var userFromRepo = await this._userApplication.GetBy(id);
                var userForDetailed = _mapper.Map<UserForDetailed>(userFromRepo);

                return Ok(userForDetailed);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UserForUpdate userForUpdateDto) 
        {
            if(this.isLoggedUserAbleToReadAndUpdateUserData(id))
            {
                var userFromRepo = await _userApplication.GetBy(id);

                this._mapper.Map(userForUpdateDto, userFromRepo);

                if(await this._userApplication.SaveAll())
                    return Ok();
                else
                    return BadRequest();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("{id}/SetToAdmin")]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> SetToAdmin(Guid id) 
        {
            if(await this._userApplication.SetToAdmin(id))
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            var userFromRepo = await _userApplication.GetBy(id);

            if(await this._userApplication.Delete(id))
                return Ok();
            else
                return BadRequest();
        }

        private bool isLoggedUserAbleToReadAndUpdateUserData(Guid idFromRequest)
        {
            return idFromRequest.ToString() == User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        private bool isLoggedUserAdmin()
        {
            return User.FindFirst(ClaimTypes.Role).Value == "admin";
        }
    }
}