using Microsoft.AspNetCore.Mvc;
using SHFT.Application.Common.Models;
using SHFT.Application.FallowedPosts.Commands.Create;
using SHFT.Application.FallowedPosts.Commands.Delete;
using SHFT.Application.FallowedPosts.Queries.GetFallowedPosts;
using SHFT.Application.Posts.Queries.Dtos;

namespace SHFT.Api.Controllers;

public class FallowedPostsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel<List<PostDto>>>> GetList([FromQuery] GetFallowedPostsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<long>>> Create([FromBody] CreateFallowedPostCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteFallowedPostCommand { PostId = id });
        return NoContent();
    }
}