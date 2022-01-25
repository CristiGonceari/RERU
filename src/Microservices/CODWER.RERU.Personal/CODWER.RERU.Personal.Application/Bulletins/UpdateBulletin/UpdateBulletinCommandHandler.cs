using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Bulletins.UpdateBulletin
{
    public class UpdateBulletinCommandHandler: IRequestHandler<UpdateBulletinCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateBulletinCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBulletinCommand request, CancellationToken cancellationToken)
        {
            var toUpdate = await _appDbContext.Bulletins
                .Include(x => x.LivingAddress)
                .Include(x => x.BirthPlace)
                .Include(x => x.ResidenceAddress)
                .FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, toUpdate);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
