using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.EditTestStatus
{
    public class EditTestStatusCommandHandler : IRequestHandler<EditTestStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContex;

        public EditTestStatusCommandHandler(AppDbContext appDbContex)
        {
            _appDbContex = appDbContex;
        }

        public async Task<Unit> Handle(EditTestStatusCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContex.Tests
                .Include(x => x.TestType)
                .FirstAsync(x => x.Id == request.TestId);

            test.TestStatus = request.Status;
            await _appDbContex.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
