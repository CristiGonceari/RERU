using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.EditSolicitedTestStatus
{
    public class EditSolicitedTestStatusCommandHandler : IRequestHandler<EditSolicitedTestStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public EditSolicitedTestStatusCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(EditSolicitedTestStatusCommand request, CancellationToken cancellationToken)
        {
            var solicitedTest = await _appDbContext.SolicitedTests.FirstAsync(x => x.Id == request.Id);
            solicitedTest.SolicitedTestStatus = request.Status;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
