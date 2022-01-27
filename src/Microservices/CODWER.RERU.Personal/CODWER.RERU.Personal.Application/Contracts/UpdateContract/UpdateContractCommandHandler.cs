using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contracts.UpdateContract
{
    public class UpdateContractCommandHandler : IRequestHandler<UpdateContractCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateContractCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
        {
            var toUpdate = await _appDbContext.Contracts.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, toUpdate);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
