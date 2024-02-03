using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Interfaces;
using SHFT.Application.Common.Models;
using SHFT.Application.Posts.Queries.Dtos;
using SHFT.Application.Posts.Queries.GetPost;

namespace SHFT.Application.Posts.Queries.GetPosts;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, BaseResponseModel<List<PostDto>>>
{
    private readonly IApplicationContext _context;
    private readonly ILogger<GetPostQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetPostsQueryHandler(IApplicationContext context, ILogger<GetPostQueryHandler> logger, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<BaseResponseModel<List<PostDto>>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var postQuery = _context.Posts
            .Where(c=>(request.Title == null || c.Title.ToLower().Contains(request.Title.ToLower())))
            .AsQueryable();

        if (request.Personal)
        {
            postQuery = postQuery.Where(c => c.UserId == _currentUserService.UserId);
        }

        List<PostDto> posts = await postQuery
            .Include(c=>c.User)
            .OrderByDescending(c=>c.CreatedAt)
            .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        List<long> fallowedPostsIds = await _context.FallowedPosts
            .Where(c => c.UserId == _currentUserService.UserId)
            .Select(c=>c.PostId)
            .ToListAsync(cancellationToken);
        
        posts.ForEach(post => { post.Fallowed = fallowedPostsIds.Contains(post.Id); });

        _logger.LogCritical("Gönderiler veritabanından başarıyla çekildi.");
        return BaseResponseModel<List<PostDto>>.Success(posts, "Gönderiler veritabanından başarıyla çekildi.");
    }
}