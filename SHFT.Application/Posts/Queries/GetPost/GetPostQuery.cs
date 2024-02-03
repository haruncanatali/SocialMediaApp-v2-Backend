using MediatR;
using SHFT.Application.Common.Models;
using SHFT.Application.Posts.Queries.Dtos;

namespace SHFT.Application.Posts.Queries.GetPost;

public class GetPostQuery : IRequest<BaseResponseModel<PostDto>>
{
    public long Id { get; set; }
}