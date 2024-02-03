using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Interfaces;
using SHFT.Application.Common.Models;
using SHFT.Application.Posts.Queries.Dtos;

namespace SHFT.Application.FallowedPosts.Queries.GetFallowedPosts;

public class GetFallowedPostsQueryHandler : IRequestHandler<GetFallowedPostsQuery, BaseResponseModel<List<PostDto>>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<GetFallowedPostsQueryHandler> _logger;

    public GetFallowedPostsQueryHandler(IApplicationContext context, IMapper mapper, ILogger<GetFallowedPostsQueryHandler> logger, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<BaseResponseModel<List<PostDto>>> Handle(GetFallowedPostsQuery request, CancellationToken cancellationToken)
    {
        List<long> fallowedPostsIds = await _context.FallowedPosts
            .Where(c => c.UserId == _currentUserService.UserId)
            .Select(c => c.PostId)
            .ToListAsync(cancellationToken);

        if (fallowedPostsIds.Count > 0)
        {
            List<PostDto> posts = await _context.Posts
                .Where(c => fallowedPostsIds.Contains(c.Id))
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            
            _logger.LogCritical("Takip edilen gönderiler başarıyla veritabanından çekildi.");
            return BaseResponseModel<List<PostDto>>.Success(posts,"Takip edilen gönderiler başarıyla veritabanından çekildi.");
        }
        
        _logger.LogCritical("Takip edilen gönderi bulunamadı.");
        return BaseResponseModel<List<PostDto>>.Success(null,"Takip edilen gönderi bulunamadı.");
    }
}