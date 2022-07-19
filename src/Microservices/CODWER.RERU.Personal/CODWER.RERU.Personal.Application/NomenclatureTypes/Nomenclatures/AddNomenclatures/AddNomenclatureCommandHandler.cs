using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using MediatR;
using RERU.Data.Entities.PersonalEntities.NomenclatureType;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.AddNomenclatures
{
    public class AddNomenclatureCommandHandler : IRequestHandler<AddNomenclatureCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddNomenclatureCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddNomenclatureCommand request, CancellationToken cancellationToken)
        {
            var mappedItem = _mapper.Map<NomenclatureType>(request.Data);
            mappedItem.IsActive = true;

            await _appDbContext.NomenclatureTypes.AddAsync(mappedItem);
            await _appDbContext.SaveChangesAsync();

            return mappedItem.Id;
        }
    }
}
