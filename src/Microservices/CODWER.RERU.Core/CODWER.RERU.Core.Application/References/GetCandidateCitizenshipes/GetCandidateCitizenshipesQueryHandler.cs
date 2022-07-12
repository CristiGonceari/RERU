using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.References.GetCandidateCitizenshipes
{
    public class GetCandidateCitizenshipesQueryHandler : IRequestHandler<GetCandidateCitizenshipesQuery, List<SelectValue>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetCandidateCitizenshipesQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectValue>> Handle(GetCandidateCitizenshipesQuery request, CancellationToken cancellationToken)
        {
            var citizenships = await _appDbContext.CandidateCitizens.ToListAsync();

            var mapper = _mapper.Map<List<SelectValue>>(citizenships);

            return mapper;
        }
    }
}
