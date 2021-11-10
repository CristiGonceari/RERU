using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.EventTestTypes.GetNoAssignedTestTypes
{
    public class GetNoAssignedTestTypesQueryHandler : IRequestHandler<GetNoAssignedTestTypesQuery, List<TestTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetNoAssignedTestTypesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<TestTypeDto>> Handle(GetNoAssignedTestTypesQuery request, CancellationToken cancellationToken)
        {
            var testTypes = _appDbContext.TestTypes
                .Include(x => x.EventTestTypes)
                .Where(x => !x.EventTestTypes.Any(e => e.EventId == request.EventId))
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                testTypes = testTypes.Where(x => x.Name.Contains(request.Keyword));
            }
            var answer = await testTypes.ToListAsync();

            return _mapper.Map<List<TestTypeDto>>(answer);
        }
    }
}
