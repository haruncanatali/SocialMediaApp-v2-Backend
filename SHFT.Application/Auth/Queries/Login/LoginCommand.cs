using MediatR;
using Microsoft.AspNetCore.Identity;
using SHFT.Application.Auth.Queries.Login.Dtos;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Interfaces;
using SHFT.Application.Common.Managers;
using SHFT.Application.Common.Mappings;
using SHFT.Application.Common.Models;
using SHFT.Domain.Identity;
using Microsoft.Extensions.Logging;

namespace SHFT.Application.Auth.Queries.Login
{
    public class LoginCommand : IRequest<BaseResponseModel<LoginDto>>, IMapFrom<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<LoginCommand, BaseResponseModel<LoginDto>>
        {
            private readonly SignInManager<User> _signInManager;

            private readonly TokenManager _tokenManager;
            private readonly UserManager<User> _userManager;
            private readonly IApplicationContext _context;
            private readonly ILogger<LoginCommand> _logger;

            public Handler(SignInManager<User> signInManager, TokenManager tokenManager, UserManager<User> userManager,
                IApplicationContext context, ILogger<LoginCommand> logger)
            {
                _signInManager = signInManager;
                _tokenManager = tokenManager;
                _userManager = userManager;
                _context = context;
                _logger = logger;
            }

            public async Task<BaseResponseModel<LoginDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                LoginDto loginViewModel = new LoginDto();
                User? appUser = await _userManager.FindByEmailAsync(request.UserName);
                if (appUser != null)
                {
                    SignInResult result = await _signInManager.PasswordSignInAsync(appUser.UserName, request.Password,
                        false, false);

                    if (result.Succeeded)
                    {
                        loginViewModel = await _tokenManager.GenerateToken(appUser);
                        appUser.RefreshToken = loginViewModel.RefreshToken;
                        appUser.RefreshTokenExpiredTime = loginViewModel.RefreshTokenExpireTime;
                        await _userManager.UpdateAsync(appUser);
                        _logger.LogCritical($"Kullanıcı başarıyla giriş yaptı. Kullanıcı :{request.UserName}");
                        return BaseResponseModel<LoginDto>.Success(data: loginViewModel);
                    }
                    else
                    {
                        _logger.LogCritical($"Kullanıcı girişi başarısız oldu. kullanıcı :{request.UserName}");
                        throw new BadRequestException($"Kullanıcı girişi başarısız oldu. kullanıcı :{request.UserName}");
                    }
                }
                
                _logger.LogCritical($"Giriş yapılmak istenen kullanıcı bulunamadı. Kullanıcı adı :{request.UserName}");
                throw new BadRequestException($"Giriş yapılmak istenen kullanıcı bulunamadı. Kullanıcı adı :{request.UserName}");
            }
        }
    }
}