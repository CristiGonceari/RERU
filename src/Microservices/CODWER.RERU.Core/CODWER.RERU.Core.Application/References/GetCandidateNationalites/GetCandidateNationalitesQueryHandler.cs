using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.References.GetCandidateNationalites
{
    public class GetCandidateNationalitesQueryHandler : IRequestHandler<GetCandidateNationalitesQuery, List<SelectValue>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetCandidateNationalitesQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectValue>> Handle(GetCandidateNationalitesQuery request, CancellationToken cancellationToken)
        {
            var nationalities = await _appDbContext.CandidateNationalities.OrderBy(x => x.NationalityName).ToListAsync();

            var mapper = _mapper.Map<List<SelectValue>>(nationalities);

            return mapper;
        }
    }
}
