using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.KinshipRelationCriminalDatas.UpdateKinshipRelationCriminalData
{
    public class UpdateKinshipRelationCriminalDataCommandHandler : IRequestHandler<UpdateKinshipRelationCriminalDataCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateKinshipRelationCriminalDataCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateKinshipRelationCriminalDataCommand request, CancellationToken cancellationToken)
        {
            var toUpdate = await _appDbContext.KinshipRelationCriminalDatas
                .FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, toUpdate);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
