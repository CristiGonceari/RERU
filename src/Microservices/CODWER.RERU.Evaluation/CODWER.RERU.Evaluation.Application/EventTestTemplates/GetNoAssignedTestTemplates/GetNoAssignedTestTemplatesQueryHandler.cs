using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.GetNoAssignedTestTemplates
{
    public class GetNoAssignedTestTemplatesQueryHandler : IRequestHandler<GetNoAssignedTestTemplatesQuery, List<TestTemplateDto>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetNoAssignedTestTemplatesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<TestTemplateDto>> Handle(GetNoAssignedTestTemplatesQuery request, CancellationToken cancellationToken)
        {
            var testTemplates = _appDbContext.TestTemplates
                .Include(x => x.EventTestTemplates)
                .Where(x => !x.EventTestTemplates.Any(e => e.EventId == request.EventId))
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                testTemplates = testTemplates.Where(x => x.Name.Contains(request.Keyword));
            }
            var answer = await testTemplates.ToListAsync();

            return _mapper.Map<List<TestTemplateDto>>(answer);
        }
    }
}
