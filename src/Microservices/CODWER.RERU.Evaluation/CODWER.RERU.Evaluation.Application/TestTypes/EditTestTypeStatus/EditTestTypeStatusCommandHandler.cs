using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.EditTestTypeStatus
{
    public class EditTestTypeStatusCommandHandler : IRequestHandler<EditTestTypeStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public EditTestTypeStatusCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(EditTestTypeStatusCommand request, CancellationToken cancellationToken)
        {
            var updateTestType = await _appDbContext.TestTypes.FirstAsync(x => x.Id == request.Data.TestTypeId);
            updateTestType.Status = request.Data.Status;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
