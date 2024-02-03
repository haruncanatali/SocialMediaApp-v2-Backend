using AutoMapper;
using SHFT.Application.Common.Mappings;
using SHFT.Domain.Enums;
using SHFT.Domain.Identity;

namespace SHFT.Application.Users.Queries.GetUserDetail
{
    public class UserDetailDto : IMapFrom<User>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string ProfilePhoto { get; set; }
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailDto>();
        }
    }
}