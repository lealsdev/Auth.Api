using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Api.Dto;
using Auth.Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly IMapper _mapper;
        public UserController(IUserApplication userApplication, IMapper mapper)
        {
            this._mapper = mapper;
            this._userApplication = userApplication;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await this._userApplication.Get();
            var usersForList = _mapper.Map<List<UserForList>>(users);

            return Ok(usersForList);
        }

        [HttpGet("id", Name="GetBy")]
        public async Task<IActionResult> GetBy(Guid id)
        {
            var users = await this._userApplication.Get();
            var usersForList = _mapper.Map<List<UserForList>>(users);

            return Ok(usersForList);
        }
    }
}