using System;

namespace Auth.Api.Dto
{
    public class UserForList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Claims { get; set; }
    }
}