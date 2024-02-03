using MediatR;
using SHFT.Application.Common.Models;
using SHFT.Application.Posts.Queries.Dtos;

namespace SHFT.Application.FallowedPosts.Queries.GetFallowedPosts;

public class GetFallowedPostsQuery : IRequest<BaseResponseModel<List<PostDto>>>
{
    
}