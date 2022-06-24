using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Bulletin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Bulletins.GetUserProfileBulletin
{
    public class GetUserProfileBulletinQueryHandler :IRequestHandler<GetUserProfileBulletinQuery, BulletinDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetUserProfileBulletinQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<BulletinDto> Handle(GetUserProfileBulletinQuery request, CancellationToken cancellationToken)
        {
            var bulletin = await _appDbContext.Bulletins
                                                .Include(b => b.BirthPlace)
                                                .Include(b => b.ResidenceAddress)
                                                .Include(b => b.ParentsResidenceAddress)
                                                .FirstOrDefaultAsync(b => b.UserProfileId == request.UserProfileId);

            return _mapper.Map<BulletinDto>(bulletin);
        }
    }
}
