using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Ranks.AddRank
{
    public class AddRankCommandHandler : IRequestHandler<AddRankCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddRankCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddRankCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Rank>(request.Data);

            await _appDbContext.Ranks.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
