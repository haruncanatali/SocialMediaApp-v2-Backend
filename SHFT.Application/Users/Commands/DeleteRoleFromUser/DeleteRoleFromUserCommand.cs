using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Models;
using SHFT.Domain.Identity;

namespace SHFT.Application.Users.Commands.DeleteRoleFromUser;

public class DeleteRoleFromUserCommand : IRequest<BaseResponseModel<Unit>>
{
    public long UserId { get; set; }
    public long RoleId { get; set; }
    
    public class Handler : IRequestHandler<DeleteRoleFromUserCommand, BaseResponseModel<Unit>>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<DeleteRoleFromUserCommand> _logger;

        public Handler(UserManager<User> userManager, ILogger<DeleteRoleFromUserCommand> logger, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                _logger.LogCritical($"İlgili rolden silinmek istenen kullanıcı bulunamadı. ID:{request.UserId}");
                throw new BadRequestException(
                    $"İlgili rolden silinmek istenen kullanıcı bulunamadı. ID:{request.UserId}");
            }

            Role? role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

            if (role == null)
            {
                _logger.LogCritical($"Rolden silinmek istenen kullanıcı için silinecek rol bulunamadı. ID:{request.RoleId}");
                throw new BadRequestException(
                    $"Rolden silinmek istenen kullanıcı için silinecek rol bulunamadı. ID:{request.RoleId}");
            }

            await _userManager.RemoveFromRoleAsync(user, role.Name!);
            _logger.LogCritical($"Kullanıcı başarıyla {role.Name} rolünden silindi. USERID:{request.UserId}");
            return BaseResponseModel<Unit>.Success(Unit.Value,$"Kullanıcı başarıyla {role.Name} rolünden silindi. USERID:{request.UserId}");
        }
    }
}