using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Interfaces;
using SHFT.Application.Common.Models;
using SHFT.Domain.Entities;

namespace SHFT.Application.Posts.Commands.Delete;

public class DeletePostCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeletePostCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<DeletePostCommand> _logger;

        public Handler(IApplicationContext context, ILogger<DeletePostCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            Post? post = await _context.Posts
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (post == null)
            {
                _logger.LogCritical($"{request.Id} ' li gönderi silinemedi. Gönderi bulunamadı.");
                throw new BadRequestException($"{request.Id} ' li gönderi silinemedi. Gönderi bulunamadı.");
            }

            List<FallowedPost> fallowedPosts = await _context.FallowedPosts
                .Where(c => c.PostId == request.Id)
                .ToListAsync(cancellationToken);

            if (fallowedPosts.Count > 0)
            {
                _context.FallowedPosts.RemoveRange(fallowedPosts);
                await _context.SaveChangesAsync(cancellationToken);
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogCritical($"{request.Id} ' li gönderi başarıyla silindi.");
            return BaseResponseModel<Unit>.Success(Unit.Value,$"{request.Id} ' li gönderi başarıyla silindi.");
        }
    }
}