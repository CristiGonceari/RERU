using AutoMapper;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithCriminalDatas.AddKinshipRelationWithCriminalData
{
    public class AddKinshipRelationWithCriminalDataCommandHandler : IRequestHandler<AddKinshipRelationWithCriminalDataCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddKinshipRelationWithCriminalDataCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddKinshipRelationWithCriminalDataCommand request, CancellationToken cancellationToken)
        {
            var mappedItem = _mapper.Map<KinshipRelationCriminalData>(request.Data);

            await _appDbContext.KinshipRelationCriminalDatas.AddAsync(mappedItem);
            await _appDbContext.SaveChangesAsync();

            return mappedItem.Id;
        }
    }
}
