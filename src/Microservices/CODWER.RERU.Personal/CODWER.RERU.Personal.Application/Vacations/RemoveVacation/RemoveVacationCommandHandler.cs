using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Vacations.RemoveVacation
{
    public class RemoveVacationCommandHandler : IRequestHandler<RemoveVacationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
     //   private readonly IFileService _fileService;

        public RemoveVacationCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            //_fileService = fileService;
        }

        public async Task<Unit> Handle(RemoveVacationCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Vacations.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Vacations.Remove(toRemove);

            if (toRemove.VacationRequestId != null)
            {
                //await _fileService.RemoveFile(toRemove.VacationRequestId.Value);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
