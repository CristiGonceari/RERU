using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.UpdateNomenclatures
{
    public class UpdateNomenclatureCommandHandler : IRequestHandler<UpdateNomenclatureCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateNomenclatureCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateNomenclatureCommand request, CancellationToken cancellationToken)
        {
            var itemToUpdate = await _appDbContext.NomenclatureTypes.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, itemToUpdate);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
