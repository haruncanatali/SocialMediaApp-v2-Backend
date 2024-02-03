using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Models;
using SHFT.Domain.Identity;

namespace SHFT.Application.Roles.Commands.Create;

public class CreateRoleCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Name { get; set; }
    
    public class Handler : IRequestHandler<CreateRoleCommand, BaseResponseModel<Unit>>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<CreateRoleCommand> _logger;

        public Handler(RoleManager<Role> roleManager, ILogger<CreateRoleCommand> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            Role? role = await _roleManager.FindByNameAsync(request.Name);

            if (role != null)
            {
                _logger.LogCritical("Rol zaten var.");
                throw new BadRequestException("Rol zaten var.");
            }

            await _roleManager.CreateAsync(new Role
            {
                Name = request.Name
            });

            _logger.LogCritical("Rol başarıyla eklendi.");
            return BaseResponseModel<Unit>.Success(Unit.Value, "Rol başarıyla eklendi.");
        }
    }
}