using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.GetSolicitedTest
{
    public class GetSolicitedTestQueryHandler : IRequestHandler<GetSolicitedTestQuery, SolicitedTestDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetSolicitedTestQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<SolicitedTestDto> Handle(GetSolicitedTestQuery request, CancellationToken cancellationToken)
        {
            var solicitedTest = await _appDbContext.SolicitedTests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<SolicitedTestDto>(solicitedTest);
        }
    }
}
