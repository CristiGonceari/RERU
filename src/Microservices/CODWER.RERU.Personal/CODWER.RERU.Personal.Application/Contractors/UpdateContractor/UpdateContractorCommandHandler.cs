using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contractors.UpdateContractor
{
    public class UpdateContractorCommandHandler : IRequestHandler<UpdateContractorCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateContractorCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateContractorCommand request, CancellationToken cancellationToken)
        {
            var contractor = await _appDbContext.Contractors
                .Include(c => c.Positions)
                .FirstAsync(r => r.Id == request.Data.Id);

            var position = contractor.Positions.LastOrDefault();

            _mapper.Map(request.Data, contractor);
            //_mapper.Map(request.Data.CurrentPosition ?? new AddEditPositionFromContractorDto(), position);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
