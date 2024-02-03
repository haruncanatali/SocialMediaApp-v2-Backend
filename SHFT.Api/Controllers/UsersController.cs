using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHFT.Application.Common.Models;
using SHFT.Application.Users.Commands.CreateUser;
using SHFT.Application.Users.Commands.DeleteRoleFromUser;
using SHFT.Application.Users.Commands.DeleteUser;
using SHFT.Application.Users.Commands.UpdateUser;
using SHFT.Application.Users.Queries.GetUserDetail;

namespace SHFT.Api.Controllers;

public class UsersController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel<UserDetailDto>>> GetById()
    {
        return Ok(await Mediator.Send(new UserDetailQuery()));
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<long>>> Create([FromForm] CreateUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<long>>> Update([FromForm] UpdateUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPut]
    [Route("DeleteRoleFromUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<long>>> Update([FromForm] DeleteRoleFromUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteUserCommand { Id = id });
        return NoContent();
    }
}