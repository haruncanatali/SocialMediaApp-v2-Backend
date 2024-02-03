using MediatR;
using Microsoft.AspNetCore.Mvc;
using SHFT.Application.Common.Models;
using SHFT.Application.Roles.Commands.AddToRole;
using SHFT.Application.Roles.Commands.Create;
using SHFT.Application.Roles.Commands.Update;
using SHFT.Application.Roles.Queries.Dtos;
using SHFT.Application.Roles.Queries.GetRoles;

namespace SHFT.Api.Controllers;

public class RolesController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<BaseResponseModel<List<RoleDto>>>> List([FromQuery]string? name)
    {
        return Ok(await Mediator.Send(new GetRolesQuery
        {
            Name = name
        }));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create(CreateRoleCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpPost]
    [Route("AddToRole")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AddToRole(AddToRoleCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<BaseResponseModel<Unit>>> Update([FromForm] UpdateRoleCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}