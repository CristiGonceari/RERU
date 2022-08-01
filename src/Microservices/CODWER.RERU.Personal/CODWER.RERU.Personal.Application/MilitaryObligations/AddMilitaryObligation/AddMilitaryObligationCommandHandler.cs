using AutoMapper;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.MilitaryObligations.AddMilitaryObligation
{
    public class AddMilitaryObligationCommandHandler : IRequestHandler<AddMilitaryObligationCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddMilitaryObligationCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddMilitaryObligationCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<MilitaryObligation>(request.Data);

            await _appDbContext.MilitaryObligations.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
