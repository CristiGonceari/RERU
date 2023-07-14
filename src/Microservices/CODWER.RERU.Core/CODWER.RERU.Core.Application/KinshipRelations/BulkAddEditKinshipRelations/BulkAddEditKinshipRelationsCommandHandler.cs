using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.KinshipRelations.BulkAddEditKinshipRelations
{
    public class BulkAddEditKinshipRelationsCommandHandler : IRequestHandler<BulkAddEditKinshipRelationsCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public BulkAddEditKinshipRelationsCommandHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(BulkAddEditKinshipRelationsCommand request, CancellationToken cancellationToken)
        {
            var kinshipRelation = await _appDbContext.KinshipRelations.Where(s => s.ContractorId == request.Data[0].ContractorId).ToListAsync();

            foreach (var relation in request.Data)
            {
                var existentRelation = kinshipRelation.FirstOrDefault(s => s.Id == relation.Id);

                if (existentRelation == null)
                {
                    var item = _mapper.Map<KinshipRelation>(relation);

                    await _appDbContext.KinshipRelations.AddAsync(item);
                }
                else
                {
                    _mapper.Map(relation, existentRelation);

                    kinshipRelation.Remove(existentRelation);
                }
            }

            if (kinshipRelation.Any())
            {
                _appDbContext.KinshipRelations.RemoveRange(kinshipRelation);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
