using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SHFT.Application.Auth.Queries.Login.Dtos;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Managers;
using SHFT.Application.Common.Models;
using SHFT.Domain.Identity;

namespace SHFT.Application.Auth.Queries.RefreshToken;

public class RefreshTokenCommand: IRequest<BaseResponseModel<LoginDto>>
{
    public string RefreshToken { get; set; }

    public class Handler : IRequestHandler<RefreshTokenCommand, BaseResponseModel<LoginDto>>
    {
        private readonly TokenManager _tokenManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RefreshTokenCommand> _logger;

        public Handler(TokenManager tokenManager, UserManager<User> userManager, ILogger<RefreshTokenCommand> logger)
        {
            _tokenManager = tokenManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<BaseResponseModel<LoginDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            User? appUser = _userManager.Users.FirstOrDefault(x => x.RefreshToken == request.RefreshToken && x.RefreshTokenExpiredTime > DateTime.Now);
            if (appUser != null)
            {
                LoginDto loginViewModel = await _tokenManager.GenerateToken(appUser);
                _logger.LogCritical($"{appUser.UserName} kullanıcısı refreshToken isteği başarıyla işlendi.");
                return BaseResponseModel<LoginDto>.Success(data: loginViewModel);
            }
            
            _logger.LogCritical($"{appUser.UserName} kullanıcısı refreshToken isteği kullanıcı bulunamadığı için işlenemedi.");
            throw new BadRequestException($"{appUser.UserName} kullanıcısı refreshToken isteği kullanıcı bulunamadığı için işlenemedi.");
        }
    }
}