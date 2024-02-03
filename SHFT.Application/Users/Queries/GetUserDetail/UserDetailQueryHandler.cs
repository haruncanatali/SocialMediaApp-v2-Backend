using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHFT.Application.Common.Exceptions;
using SHFT.Application.Common.Interfaces;

namespace SHFT.Application.Users.Queries.GetUserDetail
{
    public class UserDetailQueryHandler : IRequestHandler<UserDetailQuery, UserDetailDto>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserDetailQueryHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public UserDetailQueryHandler(IApplicationContext context, IMapper mapper, ILogger<UserDetailQueryHandler> logger, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<UserDetailDto> Handle(UserDetailQuery request, CancellationToken cancellationToken)
        {
            UserDetailDto? user = await _context.Users
                .ProjectTo<UserDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == _currentUserService.UserId, cancellationToken);

            if (user == null)
            {
                _logger.LogCritical($"(TKGG-0) Kullanıcı bulunamadı. ID:{_currentUserService.UserId}");
                throw new BadRequestException($"(TKGG-0) Kullanıcı bulunamadı. ID:{_currentUserService.UserId}");
            }
            
            long roleId = await _context.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId)
                .FirstOrDefaultAsync(cancellationToken);
            user.Roles = await _context.Roles.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync(cancellationToken);
            _logger.LogCritical($"Tekil kullanıcı görüntüleme girişiminde bulunuldu. UserID:{_currentUserService.UserId}");
            return user;
        }
    }
}