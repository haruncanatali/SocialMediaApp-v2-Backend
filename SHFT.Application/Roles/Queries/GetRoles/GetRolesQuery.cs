using MediatR;
using SHFT.Application.Common.Models;
using SHFT.Application.Roles.Queries.Dtos;

namespace SHFT.Application.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<BaseResponseModel<List<RoleDto>>>
{
    public string? Name { get; set; }
}