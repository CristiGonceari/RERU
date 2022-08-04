using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.References.GetStudyTypes
{
    public class GetStudyTypesQueryHandler : IRequestHandler<GetStudyTypesQuery, List<SelectValue>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetStudyTypesQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectValue>> Handle(GetStudyTypesQuery request, CancellationToken cancellationToken)
        {
            var nationalities = await _appDbContext.StudyTypes.ToListAsync();

            var mapper = _mapper.Map<List<SelectValue>>(nationalities);

            return mapper;
        }
    }
}
