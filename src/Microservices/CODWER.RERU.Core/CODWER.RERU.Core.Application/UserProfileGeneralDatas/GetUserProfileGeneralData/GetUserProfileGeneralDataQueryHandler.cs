using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.UserProfileGeneralDatas;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfileGeneralDatas.GetUserProfileGeneralData
{
    public class GetUserProfileGeneralDataQueryHandler : IRequestHandler<GetUserProfileGeneralDataQuery, UserProfileGeneralDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetUserProfileGeneralDataQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<UserProfileGeneralDataDto> Handle(GetUserProfileGeneralDataQuery request, CancellationToken cancellationToken)
        {
            var generalDatas = await _appDbContext.UserProfileGeneralDatas
                                                .Include(upgd => upgd.CandidateNationality)
                                                .Include(upgd => upgd.CandidateCitizenship)
                                                .FirstOrDefaultAsync(b => b.UserProfileId == request.UserProfileId);

            var mappedGeneralDatas = _mapper.Map<UserProfileGeneralDataDto>(generalDatas);

            return mappedGeneralDatas;
        }
    }
}
