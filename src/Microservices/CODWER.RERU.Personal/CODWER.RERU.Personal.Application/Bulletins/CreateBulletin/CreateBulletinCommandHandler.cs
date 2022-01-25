using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.IdentityDocuments;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Bulletins.CreateBulletin
{
    public class CreateBulletinCommandHandler : IRequestHandler<CreateBulletinCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CreateBulletinCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateBulletinCommand request, CancellationToken cancellationToken)
        {
            var mappedItem = _mapper.Map<Bulletin>(request.Data);

            await _appDbContext.Bulletins.AddAsync(mappedItem);
            await _appDbContext.SaveChangesAsync();

            return mappedItem.Id;
        }
    }
}
