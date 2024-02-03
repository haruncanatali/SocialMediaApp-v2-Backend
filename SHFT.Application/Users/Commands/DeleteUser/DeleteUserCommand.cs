using MediatR;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Interfaces;
using SHFT.Application.Common.Models;

namespace SHFT.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<BaseResponseModel<Unit>>
    {
        public long Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteUserCommand, BaseResponseModel<Unit>>
        {
            private readonly IApplicationContext _context;
            private readonly ICurrentUserService _currentUserService;
            private readonly ILogger<DeleteUserCommand> _logger;

            public DeleteCategoryCommandHandler(IApplicationContext context, ICurrentUserService currentUserService, ILogger<DeleteUserCommand> logger)
            {
                _context = context;
                _currentUserService = currentUserService;
                _logger = logger;
            }

            public async Task<BaseResponseModel<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Users
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    _logger.LogCritical($"Silinmek istenen kullanıcı bulunamadı. ID:{request.Id}");
                    throw new BadRequestException($"Silinmek istenen kullanıcı bulunamadı. ID:{request.Id}");
                }

                entity.DeletedBy = _currentUserService.UserId;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogCritical($"Kullanıcı başarıyla silindi. ID:{request.Id}");
                return BaseResponseModel<Unit>.Success(Unit.Value, $"Kullanıcı başarıyla silindi. ID:{request.Id}");
            }
        }
    }
}
