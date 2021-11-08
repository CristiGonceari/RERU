using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.DeleteTestType
{
    public class DeleteTestTypeCommandHandler : IRequestHandler<DeleteTestTypeCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteTestTypeCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteTestTypeCommand request, CancellationToken cancellationToken)
        {
            var testType = await _appDbContext.TestTypes.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.TestTypes.Remove(testType);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
