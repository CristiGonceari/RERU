using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.BulkAddEditKinshipRelationWithUserProfiles
{
    public class BulkAddEditKinshipRelationWithUserProfilesCommandHandler : IRequestHandler<BulkAddEditKinshipRelationWithUserProfilesCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public BulkAddEditKinshipRelationWithUserProfilesCommandHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(BulkAddEditKinshipRelationWithUserProfilesCommand request, CancellationToken cancellationToken)
        {
            var kinshipRelation = await _appDbContext.KinshipRelationWithUserProfiles.ToListAsync();

            foreach (var relation in request.Data)
            {
                var existentRelation = kinshipRelation.FirstOrDefault(s => s.Id == relation.Id);

                if (existentRelation == null)
                {
                    var item = _mapper.Map<KinshipRelationWithUserProfile>(relation);

                    await _appDbContext.KinshipRelationWithUserProfiles.AddAsync(item);
                }
                else
                {
                    _mapper.Map(relation, existentRelation);

                    kinshipRelation.Remove(existentRelation);
                }
            }

            if (kinshipRelation.Any())
            {
                _appDbContext.KinshipRelationWithUserProfiles.RemoveRange(kinshipRelation);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
