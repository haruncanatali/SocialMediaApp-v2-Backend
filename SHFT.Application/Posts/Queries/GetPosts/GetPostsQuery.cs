using MediatR;
using SHFT.Application.Common.Models;
using SHFT.Application.Posts.Queries.Dtos;

namespace SHFT.Application.Posts.Queries.GetPosts;

public class GetPostsQuery : IRequest<BaseResponseModel<List<PostDto>>>
{
    public bool Personal { get; set; } = false;
    public string? Title { get; set; }
}