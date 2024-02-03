using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Models;
using SHFT.Domain.Identity;

namespace SHFT.Application.Roles.Commands.AddToRole;

public class AddToRoleCommand : IRequest<BaseResponseModel<Unit>>
{
    public long RoleId { get; set; }
    public long UserId { get; set; }
    
    public class Handler : IRequestHandler<AddToRoleCommand, BaseResponseModel<Unit>>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<AddToRoleCommand> _logger;

        public Handler(UserManager<User> userManager, RoleManager<Role> roleManager, ILogger<AddToRoleCommand> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(AddToRoleCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user != null)
            {
                Role? role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name!);
                }
                else
                {
                    Role? normalRole = await _roleManager.FindByNameAsync("Normal");
                    if (normalRole != null)
                    {
                        await _userManager.AddToRoleAsync(user, "Normal");
                    }
                    else
                    {
                        await _roleManager.CreateAsync(new Role
                        {
                            Name = "Normal"
                        });
                        await _userManager.AddToRoleAsync(user, "Normal");
                    }
                }
                
                _logger.LogCritical("Kullanıcı role eklendi.");
                return BaseResponseModel<Unit>.Success(Unit.Value, "Kullanıcı role eklendi.");
            }

            _logger.LogCritical("Tanımsız kullanıcı bilgisi.");
            throw new BadRequestException("Tanımsız kullanıcı bilgisi.");
        }
    }
}