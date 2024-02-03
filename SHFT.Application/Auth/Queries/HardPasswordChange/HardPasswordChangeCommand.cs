using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Interfaces;
using SHFT.Application.Common.Models;
using SHFT.Domain.Identity;

namespace SHFT.Application.Auth.Queries.HardPasswordChange;

public class HardPasswordChangeCommand : IRequest<BaseResponseModel<User>>
{
    public string Password { get; set; }

    public class Handler : IRequestHandler<HardPasswordChangeCommand, BaseResponseModel<User>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<HardPasswordChangeCommand> _logger;
        private readonly ICurrentUserService _currentUserService;

        public Handler(UserManager<User> userManager, ILogger<HardPasswordChangeCommand> logger, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponseModel<User>> Handle(HardPasswordChangeCommand request, CancellationToken cancellationToken)
        {
            User? appUser = _userManager.Users.FirstOrDefault(x => x.Id == _currentUserService.UserId);
            if (appUser != null)
            {
                var removeResult = await _userManager.RemovePasswordAsync(appUser);
                if (removeResult.Succeeded)
                {
                    var addResult = await _userManager.AddPasswordAsync(appUser, request.Password);
                    if (addResult.Succeeded)
                    {
                        _logger.LogCritical($"Şifre değiştirildi. Güncelleme yapılan kullanıcı : {appUser.UserName}");
                        return BaseResponseModel<User>.Success(appUser,$"Şifre değiştirildi. Güncelleme yapılan kullanıcı : {appUser.UserName}");
                    }
                    else
                    {
                        _logger.LogCritical($"(HPCC-0) Şifre değiştirilemedi. Hata meydana geldi. Güncelleme yapılamıyan kullanıcı : {appUser.UserName}");
                        throw new BadRequestException($"Şifre değiştirilemedi. Hata meydana geldi. Güncelleme yapılamıyan kullanıcı : {appUser.UserName}");
                    }
                }
                else
                {
                    _logger.LogCritical($"(HPCC-1) Şifre değiştirilemedi. Hata meydana geldi. Güncelleme yapılamıyan kullanıcı : {appUser.UserName}");
                    throw new BadRequestException("Şifre Silinemedi!");
                }
            }
            
            _logger.LogCritical($"(HPCC-2) Şifre değiştirilemedi. Kullanıcı bulunamadı. Güncellenmek istenen kullanıcı ID:{_currentUserService.UserId}");
            throw new BadRequestException( $"(HPCC-2) Şifre değiştirilemedi. Kullanıcı bulunamadı. Güncellenmek istenen kullanıcı ID:{_currentUserService.UserId}");
        }
    }
}