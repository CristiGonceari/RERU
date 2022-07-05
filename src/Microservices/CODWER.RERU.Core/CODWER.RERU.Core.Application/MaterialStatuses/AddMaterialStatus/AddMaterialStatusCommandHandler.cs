using AutoMapper;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.MaterialStatuses.AddMaterialStatus
{
    public class AddMaterialStatusCommandHandler : IRequestHandler<AddMaterialStatusCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddMaterialStatusCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async  Task<int> Handle(AddMaterialStatusCommand request, CancellationToken cancellationToken)
        {
            var mappedItem = _mapper.Map<MaterialStatus>(request.Data);

            await _appDbContext.MaterialStatuses.AddAsync(mappedItem);
            await _appDbContext.SaveChangesAsync();

            return mappedItem.Id;
        }
    }
}
