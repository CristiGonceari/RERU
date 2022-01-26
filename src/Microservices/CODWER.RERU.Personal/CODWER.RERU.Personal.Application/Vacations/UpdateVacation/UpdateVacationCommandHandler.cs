using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Vacations.UpdateVacation
{
    public class UpdateVacationCommandHandler : IRequestHandler<UpdateVacationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateVacationCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateVacationCommand request, CancellationToken cancellationToken)
        {
            //var item = await _appDbContext.Vacations.FirstAsync(x => x.Id == request.Data.Id);

            //_mapper.Map(request.Data, item);
            //await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
