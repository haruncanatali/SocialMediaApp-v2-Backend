using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHFT.Application.Auth.Queries.HardPasswordChange;
using SHFT.Application.Auth.Queries.Login;
using SHFT.Application.Auth.Queries.Login.Dtos;
using SHFT.Application.Auth.Queries.RefreshToken;
using SHFT.Application.Common.Models;

namespace SHFT.Api.Controllers;

public class AuthController : BaseController
{
    [AllowAnonymous]
    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<BaseResponseModel<LoginDto>>> Login([FromForm]LoginCommand loginModel)
    {
        BaseResponseModel<LoginDto> loginResponse = await Mediator.Send(loginModel);
        return Ok(loginResponse);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("refreshtoken")]
    public async Task<ActionResult<BaseResponseModel<LoginDto>>> RefreshToken(string refreshToken)
    {
        return Ok(await Mediator.Send(new RefreshTokenCommand { RefreshToken = refreshToken }));
    }
    
    [HttpPost]
    [Route("hard-password-change")]
    public async Task<IActionResult> HardPasswordChange(HardPasswordChangeCommand request)
    {
        await Mediator.Send(request);
        return Ok();
    }
}