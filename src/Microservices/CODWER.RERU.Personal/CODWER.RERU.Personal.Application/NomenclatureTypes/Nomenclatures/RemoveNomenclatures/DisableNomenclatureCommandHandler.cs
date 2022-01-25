using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Personal.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.RemoveNomenclatures
{
    public class DisableNomenclatureCommandHandler : IRequestHandler<DisableNomenclatureCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DisableNomenclatureCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DisableNomenclatureCommand request, CancellationToken cancellationToken)
        {
            var nomenclatureType = await _appDbContext.NomenclatureTypes.FirstAsync(x => x.Id == request.Id);
            var nomenclatureRecords = _appDbContext.NomenclatureRecords.Where(x=>x.NomenclatureTypeId == request.Id).AsQueryable();

            await nomenclatureRecords.ForEachAsync(x => x.IsActive = false);
            nomenclatureType.IsActive = false;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
