using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeRules
{
    public class GetTestTypeRulesQueryHandler : IRequestHandler<GetTestTypeRulesQuery, RulesDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestTypeRulesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<RulesDto> Handle(GetTestTypeRulesQuery request, CancellationToken cancellationToken)
        {
            var testType = await _appDbContext.TestTypes.FirstOrDefaultAsync(x => x.Id == request.TestTypeId);

            var answer = _mapper.Map<RulesDto>(testType);

            if (!string.IsNullOrEmpty(testType.Rules))
            {
                var base64EncodedBytes = Convert.FromBase64String(testType.Rules);
                answer.Rules = Encoding.UTF8.GetString(base64EncodedBytes);
            }

            return answer;
        }
    }
}
