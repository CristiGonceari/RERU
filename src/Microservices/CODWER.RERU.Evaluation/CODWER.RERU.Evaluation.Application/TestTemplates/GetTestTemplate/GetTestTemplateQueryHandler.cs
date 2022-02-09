using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplate
{
    public class GetTestTemplateQueryHandler : IRequestHandler<GetTestTemplateQuery, TestTemplateDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestTemplateQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<TestTemplateDto> Handle(GetTestTemplateQuery request, CancellationToken cancellationToken)
        {
            var testType = await _appDbContext.TestTemplates.FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<TestTemplateDto>(testType);
        }
    }
}
