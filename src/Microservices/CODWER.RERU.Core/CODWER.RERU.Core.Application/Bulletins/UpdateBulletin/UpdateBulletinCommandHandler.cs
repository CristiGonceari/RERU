using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Bulletins.UpdateBulletin
{
    public class UpdateBulletinCommandHandler : IRequestHandler<UpdateBulletinCommand, Unit>
    {
        public readonly AppDbContext _appDbContext;
        public readonly IMapper _mapper;
        public UpdateBulletinCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async  Task<Unit> Handle(UpdateBulletinCommand request, CancellationToken cancellationToken)
        {
            var bulletin = await _appDbContext.Bulletins
                                                .Include(b => b.BirthPlace)
                                                .Include(b => b.ResidenceAddress)
                                                .Include(b => b.ParentsResidenceAddress)
                                                .FirstOrDefaultAsync(b => b.Id == request.Data.Id);

            _mapper.Map(request.Data, bulletin);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
