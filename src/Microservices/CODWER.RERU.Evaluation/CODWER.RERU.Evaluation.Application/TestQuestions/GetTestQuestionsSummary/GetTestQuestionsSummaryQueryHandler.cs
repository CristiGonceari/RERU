using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestionsSummary
{
    public class GetTestQuestionsSummaryQueryHandler : IRequestHandler<GetTestQuestionsSummaryQuery, List<TestQuestionSummaryDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestQuestionsSummaryQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<TestQuestionSummaryDto>> Handle(GetTestQuestionsSummaryQuery request, CancellationToken cancellationToken)
        {
            var testQuestions = await _appDbContext.TestQuestions
                .Include(x => x.QuestionUnit)
                .Include(x => x.Test)
                    .ThenInclude(x => x.TestTemplate)
                        .ThenInclude(x => x.Settings)
                .Where(x => x.TestId == request.TestId)
                .OrderBy(x => x.Index)
                .ToListAsync();

            return _mapper.Map<List<TestQuestionSummaryDto>>(testQuestions);
        }
    }
}
