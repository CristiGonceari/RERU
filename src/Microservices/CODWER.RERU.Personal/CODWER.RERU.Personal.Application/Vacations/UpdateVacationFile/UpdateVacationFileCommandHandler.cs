using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Vacations.UpdateVacationFile
{
    public class UpdateVacationFileCommandHandler : IRequestHandler<UpdateVacationFileCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public UpdateVacationFileCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UpdateVacationFileCommand request, CancellationToken cancellationToken)
        {
            var vacation = await _appDbContext.Vacations.FirstOrDefaultAsync(v => v.Id == request.Data.VacationId);
           // var newFileId = await _fileService.AddFile(request.Data.NewFile);

            var fileToDeleteId = vacation.VacationRequestId;
           // vacation.VacationRequestId = newFileId;
            await _appDbContext.SaveChangesAsync();

           // if (fileToDeleteId != null) await _fileService.RemoveFile(fileToDeleteId.Value);

            return Unit.Value;
        }
    }
}
