using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Interfaces;
using SHFT.Application.Common.Models;
using SHFT.Domain.Entities;

namespace SHFT.Application.Posts.Commands.Update;

public class UpdatePostCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Content { get; set; }
    public string Title { get; set; }
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<UpdatePostCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly ILogger<UpdatePostCommand> _logger;

        public Handler(IApplicationContext context, ILogger<UpdatePostCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            Post? post = await _context.Posts
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (post == null)
            {
                _logger.LogCritical($"{request.Id} ' li gönderi güncellenemedi. Gönderi bulunamadı.");
                throw new BadRequestException($"{request.Id} ' li gönderi güncellenemedi. Gönderi bulunamadı.");
            }

            post.Content = request.Content;
            post.Title = request.Title;
            // TODO: _context.Update(post); edilmesi gerekiyor mu kontrol et
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogCritical($"{request.Id} ' li gönderi güncellendi.");
            return BaseResponseModel<Unit>.Success(Unit.Value,$"{request.Id} ' li gönderi güncellendi.");
        }
    }
}