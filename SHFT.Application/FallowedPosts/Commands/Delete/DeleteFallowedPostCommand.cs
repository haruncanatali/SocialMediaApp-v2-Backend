using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Interfaces;
using SHFT.Application.Common.Models;
using SHFT.Domain.Entities;

namespace SHFT.Application.FallowedPosts.Commands.Delete;

public class DeleteFallowedPostCommand : IRequest<BaseResponseModel<Unit>>
{
    public long PostId { get; set; }
    
    public class Handler : IRequestHandler<DeleteFallowedPostCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<DeleteFallowedPostCommand> _logger;

        public Handler(IApplicationContext context, ICurrentUserService currentUserService, ILogger<DeleteFallowedPostCommand> logger)
        {
            _context = context;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteFallowedPostCommand request, CancellationToken cancellationToken)
        {
            FallowedPost? fallowedPost = await _context.FallowedPosts
                .FirstOrDefaultAsync(c => c.UserId == _currentUserService.UserId && c.PostId == request.PostId,cancellationToken);

            if (fallowedPost == null)
            {
                _logger.LogCritical($"Takipten çıkılmak istenen {request.PostId} ID ' li gönderi bulunamadı.");
                throw new BadRequestException(
                    $"Takipten çıkılmak istenen {request.PostId} ID ' li gönderi bulunamadı.");
            }

            _context.FallowedPosts.Remove(fallowedPost);
            await _context.SaveChangesAsync(cancellationToken);
            return BaseResponseModel<Unit>.Success(Unit.Value,"Gönderi başarıyla takipten çıkıldı.");
        }
    }
}