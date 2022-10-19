using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.GetTestVerificationSummary
{
    public class GetTestVerificationSummaryQueryHandler : IRequestHandler<GetTestVerificationSummaryQuery, VerificationTestQuestionDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestVerificationSummaryQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<VerificationTestQuestionDataDto> Handle(GetTestVerificationSummaryQuery request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .Include(x => x.TestQuestions)
                    .ThenInclude(x => x.QuestionUnit)
                .Include(x => x.TestQuestions)
                    .ThenInclude(x => x.TestAnswers)
                .FirstAsync(x => x.Id == request.TestId);

            return new VerificationTestQuestionDataDto
            {
                TestQuestions = test.TestQuestions.OrderBy(x => x.Index).Select(x => _mapper.Map<VerificationTestQuestionSummaryDto>(x)).ToList(),
                CorrectAnswers = test.TestQuestions.Count(x => x.IsCorrect is true),
                Points = test.TestTemplate.MinPercent,
                TotalQuestions = test.TestTemplate.QuestionCount,
                Result = test.ResultStatus,
                ResultValue = test.ResultStatusValue
            };
        }
    }
}
