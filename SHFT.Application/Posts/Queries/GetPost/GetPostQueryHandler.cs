using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Interfaces;
using SHFT.Application.Common.Models;
using SHFT.Application.Posts.Queries.Dtos;

namespace SHFT.Application.Posts.Queries.GetPost;

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, BaseResponseModel<PostDto>>
{
    private readonly IApplicationContext _context;
    private readonly ILogger<GetPostQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetPostQueryHandler(IApplicationContext context, ILogger<GetPostQueryHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<PostDto>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        PostDto? post = await _context.Posts
            .Where(c => c.Id == request.Id)
            .Include(c=>c.User)
            .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        _logger.LogCritical($"{request.Id} ' li gönderi veritabanından çekildi.");
        return BaseResponseModel<PostDto>.Success(post, $"{request.Id} ' li gönderi veritabanından çekildi.");
    }
}