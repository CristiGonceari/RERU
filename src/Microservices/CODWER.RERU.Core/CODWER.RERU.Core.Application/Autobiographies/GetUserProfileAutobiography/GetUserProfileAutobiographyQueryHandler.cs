using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Autobiography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Autobiographies.GetUserProfileAutobiography
{
    public class GetUserProfileAutobiographyQueryHandler : IRequestHandler<GetUserProfileAutobiographyQuery, AutobiographyDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetUserProfileAutobiographyQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async  Task<AutobiographyDto> Handle(GetUserProfileAutobiographyQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Autobiographies
               .FirstAsync(x => x.UserProfileId == request.UserProfileId);

            return _mapper.Map<AutobiographyDto>(item);
        }
    }
}
