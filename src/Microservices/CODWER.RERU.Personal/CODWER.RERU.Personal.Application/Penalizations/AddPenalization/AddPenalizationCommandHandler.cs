using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Penalizations.AddPenalization
{
    public class AddPenalizationCommandHandler : IRequestHandler<AddPenalizationCommand, int>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddPenalizationCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddPenalizationCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Penalization>(request.Data);

            await _appDbContext.Penalizations.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
