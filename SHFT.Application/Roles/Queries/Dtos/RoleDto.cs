using AutoMapper;
using SHFT.Application.Common.Mappings;
using SHFT.Domain.Identity;

namespace SHFT.Application.Roles.Queries.Dtos;

public class RoleDto : IMapFrom<Role>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Role, RoleDto>();
    }
}