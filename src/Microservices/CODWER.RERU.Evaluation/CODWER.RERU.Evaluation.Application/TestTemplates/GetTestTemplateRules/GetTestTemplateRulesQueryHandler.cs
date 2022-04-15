using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateRules
{
    public class GetTestTemplateRulesQueryHandler : IRequestHandler<GetTestTemplateRulesQuery, RulesDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestTemplateRulesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<RulesDto> Handle(GetTestTemplateRulesQuery request, CancellationToken cancellationToken)
        {
            var testTemplate = await _appDbContext.TestTemplates.FirstOrDefaultAsync(x => x.Id == request.TestTemplateId);

            var answer = _mapper.Map<RulesDto>(testTemplate);

            if (!string.IsNullOrEmpty(testTemplate.Rules))
            {
                var base64EncodedBytes = Convert.FromBase64String(testTemplate.Rules);
                answer.Rules = Encoding.UTF8.GetString(base64EncodedBytes);
            }

            return answer;
        }
    }
}
