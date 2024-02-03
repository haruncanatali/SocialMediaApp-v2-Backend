using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Models;
using SHFT.Application.Roles.Queries.Dtos;
using SHFT.Application.Roles.Queries.GetRoles;
using SHFT.Domain.Identity;

namespace Hospital.Application.Roles.Queries.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, BaseResponseModel<List<RoleDto>>>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;
    private readonly ILogger<GetRolesQueryHandler> _logger;

    public GetRolesQueryHandler(RoleManager<Role> roleManager, IMapper mapper, ILogger<GetRolesQueryHandler> logger)
    {
        _roleManager = roleManager;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponseModel<List<RoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        List<RoleDto> roles = await _roleManager.Roles
            .Where(c => (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())))
            .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        _logger.LogCritical("Rolle başarıyla çekildi.");
        return BaseResponseModel<List<RoleDto>>.Success(roles, "Roller başarıyla çekildi.");
    }
}