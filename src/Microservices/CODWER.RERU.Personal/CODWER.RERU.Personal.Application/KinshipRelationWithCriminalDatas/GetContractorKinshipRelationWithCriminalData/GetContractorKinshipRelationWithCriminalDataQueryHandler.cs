using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithCriminalData;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithCriminalDatas.GetContractorKinshipRelationWithCriminalData
{
    public class GetContractorKinshipRelationWithCriminalDataQueryHandler : IRequestHandler<GetContractorKinshipRelationWithCriminalDataQuery, KinshipRelationWithCriminalDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetContractorKinshipRelationWithCriminalDataQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<KinshipRelationWithCriminalDataDto> Handle(GetContractorKinshipRelationWithCriminalDataQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.KinshipRelationCriminalDatas
                .FirstAsync(x => x.ContractorId == request.ContractorId);

            return _mapper.Map<KinshipRelationWithCriminalDataDto>(item);
        }
    }
}
