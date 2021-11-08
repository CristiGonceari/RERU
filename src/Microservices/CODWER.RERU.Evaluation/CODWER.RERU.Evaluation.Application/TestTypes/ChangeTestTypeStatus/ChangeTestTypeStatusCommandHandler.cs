using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.ChangeTestTypeStatus
{
    public class ChangeTestTypeStatusCommandHandler : IRequestHandler<ChangeTestTypeStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public ChangeTestTypeStatusCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(ChangeTestTypeStatusCommand request, CancellationToken cancellationToken)
        {
            var updateTestType = await _appDbContext.TestTypes.FirstAsync(x => x.Id == request.Data.TestTypeId);
            updateTestType.Status = request.Data.Status;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
