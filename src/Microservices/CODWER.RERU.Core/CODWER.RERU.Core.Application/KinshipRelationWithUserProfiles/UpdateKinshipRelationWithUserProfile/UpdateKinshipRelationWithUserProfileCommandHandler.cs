using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.UpdateKinshipRelationWithUserProfile
{
    public class UpdateKinshipRelationWithUserProfileCommandHandler : IRequestHandler<UpdateKinshipRelationWithUserProfileCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateKinshipRelationWithUserProfileCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async  Task<Unit> Handle(UpdateKinshipRelationWithUserProfileCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.KinshipRelationCriminalDatas.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, item);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
