using AutoMapper;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Autobiographies.AddAutobiography
{
    public class AddAutobiographyCommandHandler : IRequestHandler<AddAutobiographyCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddAutobiographyCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddAutobiographyCommand request, CancellationToken cancellationToken)
        {
            var mappedItem = _mapper.Map<Autobiography>(request.Data);

            await _appDbContext.Autobiographies.AddAsync(mappedItem);
            await _appDbContext.SaveChangesAsync();

            return mappedItem.Id;
        }
    }
}
