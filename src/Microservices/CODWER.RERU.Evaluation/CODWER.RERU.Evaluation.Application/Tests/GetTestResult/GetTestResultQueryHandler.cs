using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTestResult
{
    public class GetTestResultQueryHandler : IRequestHandler<GetTestResultQuery, TestResultDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestResultQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<TestResultDto> Handle(GetTestResultQuery request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.TestType)
                .Include(x => x.TestQuestions)
                .FirstOrDefaultAsync(x => x.Id == request.TestId);

            return _mapper.Map<TestResultDto>(test);
        }
    }
}
