using Hospital.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Interfaces;
using SHFT.Application.Common.Managers;
using SHFT.Application.Common.Models;
using SHFT.Domain.Enums;
using SHFT.Domain.Identity;

namespace SHFT.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<BaseResponseModel<long>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }

        public class Handler : IRequestHandler<UpdateUserCommand, BaseResponseModel<long>>
        {
            private readonly UserManager<User> _userManager;
            private readonly IApplicationContext _context;
            private readonly FileManager _fileManager;
            private readonly ILogger<UpdateUserCommand> _logger;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IApplicationContext context, FileManager fileManager, UserManager<User> userManager, ILogger<UpdateUserCommand> logger, ICurrentUserService currentUserService)
            {
                _context = context;
                _fileManager = fileManager;
                _userManager = userManager;
                _logger = logger;
                _currentUserService = currentUserService;
            }

            public async Task<BaseResponseModel<long>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    User? entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId);
                    string profilePhoto = entity.ProfilePhoto;
                    if (request.ProfilePhoto != null)
                    {
                        profilePhoto = _fileManager.Upload(request.ProfilePhoto, FileRoot.UserProfile);
                    }

                    entity.FirstName = request.FirstName;
                    entity.LastName = request.LastName;
                    entity.Gender = request.Gender;
                    entity.ProfilePhoto = profilePhoto;
                    entity.PhoneNumber = request.Phone;
                    entity.Birthdate = request.Birthdate;
                    
                    await _userManager.UpdateAsync(entity);

                    _logger.LogCritical("Kullanıcı başarıyla oluşturuldu.");
                    return BaseResponseModel<long>.Success(entity.Id);
                }
                catch (Exception e)
                {
                    throw new BadRequestException(
                        "Kullanıcı oluşturulurken hata meydana geldi. Hata: {e.Message}");
                }
            }
        }
    }
}