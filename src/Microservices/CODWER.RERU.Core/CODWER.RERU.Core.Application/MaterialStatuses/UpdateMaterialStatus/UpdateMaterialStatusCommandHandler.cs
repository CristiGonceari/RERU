using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.MaterialStatuses.UpdateMaterialStatus
{
    public class UpdateMaterialStatusCommandHandler : IRequestHandler<UpdateMaterialStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateMaterialStatusCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async  Task<Unit> Handle(UpdateMaterialStatusCommand request, CancellationToken cancellationToken)
        {
            var toUpdate = await _appDbContext.MaterialStatuses.FirstOrDefaultAsync(ms => ms.Id == request.Data.Id);

            _mapper.Map(request.Data, toUpdate);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
