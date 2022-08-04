using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Badges.AddBadge
{
    public class AddBadgeCommandHandler : IRequestHandler<AddBadgeCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddBadgeCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddBadgeCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Badge>(request.Data);

            await _appDbContext.Badges.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
