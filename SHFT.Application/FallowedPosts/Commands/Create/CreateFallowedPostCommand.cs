using MediatR;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Interfaces;
using SHFT.Application.Common.Models;
using SHFT.Domain.Entities;

namespace SHFT.Application.FallowedPosts.Commands.Create;

public class CreateFallowedPostCommand : IRequest<BaseResponseModel<Unit>>
{
    public long PostId { get; set; }
    
    public class Handler : IRequestHandler<CreateFallowedPostCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<CreateFallowedPostCommand> _logger;
        private readonly ICurrentUserService _currentUserService;

        public Handler(IApplicationContext context, ILogger<CreateFallowedPostCommand> logger, ICurrentUserService currentUserService)
        {
            _context = context;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateFallowedPostCommand request, CancellationToken cancellationToken)
        {
            await _context.FallowedPosts.AddAsync(new FallowedPost
            {
                PostId = request.PostId,
                UserId = _currentUserService.UserId
            });

            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogCritical($"{request.PostId} ID ' li gönderi başarıyla takip edildi.");
            return BaseResponseModel<Unit>.Success(Unit.Value,$"{request.PostId} ID ' li gönderi başarıyla takip edildi.");
        }
    }
}