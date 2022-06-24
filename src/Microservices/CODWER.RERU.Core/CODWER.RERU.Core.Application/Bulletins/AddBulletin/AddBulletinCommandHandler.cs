using AutoMapper;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Bulletins.AddBulletin
{
    public class AddBulletinCommandHandler : IRequestHandler<AddBulletinCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddBulletinCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddBulletinCommand request, CancellationToken cancellationToken)
        {
            var mappedBulletinData = _mapper.Map<Bulletin>(request.Data);

            await _appDbContext.Bulletins.AddAsync(mappedBulletinData);
            await _appDbContext.SaveChangesAsync();

            return mappedBulletinData.Id;
        }
    }
}
