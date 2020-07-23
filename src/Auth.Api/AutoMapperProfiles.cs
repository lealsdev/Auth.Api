using Auth.Api.Dto;
using Auth.Model;
using AutoMapper;

namespace Auth.Api
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForList>();
            CreateMap<User, UserForDetailed>();
            CreateMap<UserForRegister, User>();
            CreateMap<UserForUpdate, User>();
        }
    }
}