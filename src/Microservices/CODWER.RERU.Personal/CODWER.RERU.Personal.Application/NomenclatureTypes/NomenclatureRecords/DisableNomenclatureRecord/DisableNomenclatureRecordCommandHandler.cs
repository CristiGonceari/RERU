using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.DisableNomenclatureRecord
{
    public class DisableNomenclatureRecordCommandHandler : IRequestHandler<DisableNomenclatureRecordCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DisableNomenclatureRecordCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DisableNomenclatureRecordCommand request, CancellationToken cancellationToken)
        {
            var recordToDisable = await _appDbContext.NomenclatureRecords.FirstAsync(x => x.Id == request.Id);

            recordToDisable.IsActive = false;

            await _appDbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
