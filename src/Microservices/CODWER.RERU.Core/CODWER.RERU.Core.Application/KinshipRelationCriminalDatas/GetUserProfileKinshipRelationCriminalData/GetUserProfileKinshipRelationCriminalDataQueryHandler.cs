using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.KinshipRelationCriminalData;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.KinshipRelationCriminalDatas.GetUserProfileKinshipRelationCriminalData
{
    public class GetUserProfileKinshipRelationCriminalDataQueryHandler : IRequestHandler<GetUserProfileKinshipRelationCriminalDataQuery, KinshipRelationCriminalDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetUserProfileKinshipRelationCriminalDataQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async  Task<KinshipRelationCriminalDataDto> Handle(GetUserProfileKinshipRelationCriminalDataQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.KinshipRelationCriminalDatas
                .FirstAsync(x => x.UserProfileId == request.UserProfileId);

            return _mapper.Map<KinshipRelationCriminalDataDto>(item);
        }
    }
}
