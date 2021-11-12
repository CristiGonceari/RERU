using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var testQuestions = _appDbContext.TestQuestions
                .Include(x => x.TestAnswers)
                .Include(x => x.QuestionUnit)
                .Include(x => x.Test)
                .ThenInclude(x => x.TestType)
                .Where(x => x.TestId == request.TestId)
                .OrderBy(x => x.Index)
                .AsQueryable();

            var result = new VerificationTestQuestionDataDto();
            result.TestQuestions = await testQuestions.Select(x => _mapper.Map<VerificationTestQuestionSummaryDto>(x)).ToListAsync();

            result.CorrectAnswers = testQuestions.Where(x => (bool)x.IsCorrect).Count();
            result.Points = testQuestions.Select(x => x.Test.TestType.MinPercent).First();
            result.TotalQuestions = testQuestions.Select(x => x.Test.TestType.QuestionCount).First();
            result.Result = testQuestions.Select(x => x.Test.ResultStatus).First();

            return result;
        }
    }
}
