using MediatR;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Interfaces;
using SHFT.Application.Common.Models;
using SHFT.Domain.Entities;

namespace SHFT.Application.Posts.Commands.Create;

public class CreatePostCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Content { get; set; }
    public string Title { get; set; }
    
    public class Handler : IRequestHandler<CreatePostCommand, BaseResponseModel<Unit>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationContext _context;
        private readonly ILogger<CreatePostCommand> _logger;

        public Handler(ICurrentUserService currentUserService, IApplicationContext applicationContext, ILogger<CreatePostCommand> logger)
        {
            _currentUserService = currentUserService;
            _context = applicationContext;
            _logger = logger;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            await _context.Posts.AddAsync(new Post
            {
                UserId = _currentUserService.UserId,
                Content = request.Content,
                Title = request.Title
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogCritical("Gönderi oluşturuldu.");
            return BaseResponseModel<Unit>.Success(Unit.Value,"Gönderi oluşturuldu.");
        }
    }
}