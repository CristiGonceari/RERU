using AutoMapper;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.KinshipRelations.AddKinshipRelation
{
    public class AddKinshipRelationCommandHandler : IRequestHandler<AddKinshipRelationCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddKinshipRelationCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddKinshipRelationCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<KinshipRelation>(request.Data);

            await _appDbContext.KinshipRelations.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
