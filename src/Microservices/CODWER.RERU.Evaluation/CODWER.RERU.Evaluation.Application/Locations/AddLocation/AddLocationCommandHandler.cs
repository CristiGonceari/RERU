using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Locations.AddLocation
{
    public class AddLocationCommandHandler : IRequestHandler<AddLocationCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddLocationCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddLocationCommand request, CancellationToken cancellationToken)
        {
            var locationToCreate = _mapper.Map<Location>(request.Data);

            await _appDbContext.Locations.AddAsync(locationToCreate);

            await _appDbContext.SaveChangesAsync();

            return locationToCreate.Id;
        }
    }
}
