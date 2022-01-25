using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Bonuses.AddBonus
{
    public class AddBonusCommandHandler : IRequestHandler<AddBonusCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddBonusCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddBonusCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Bonus>(request.Data);

            await _appDbContext.Bonuses.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
