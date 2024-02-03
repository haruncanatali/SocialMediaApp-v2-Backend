using Microsoft.AspNetCore.Mvc;
using SHFT.Application.Common.Models;
using SHFT.Application.Posts.Commands.Create;
using SHFT.Application.Posts.Commands.Delete;
using SHFT.Application.Posts.Commands.Update;
using SHFT.Application.Posts.Queries.Dtos;
using SHFT.Application.Posts.Queries.GetPost;
using SHFT.Application.Posts.Queries.GetPosts;

namespace SHFT.Api.Controllers;

public class PostsController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponseModel<PostDto>>> GetById(long id = 0)
    {
        return Ok(await Mediator.Send(new GetPostQuery { Id = id }));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponseModel<List<PostDto>>>> GetList([FromQuery] GetPostsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<long>>> Create([FromBody] CreatePostCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<long>>> Update([FromBody] UpdatePostCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeletePostCommand { Id = id });
        return NoContent();
    }
}