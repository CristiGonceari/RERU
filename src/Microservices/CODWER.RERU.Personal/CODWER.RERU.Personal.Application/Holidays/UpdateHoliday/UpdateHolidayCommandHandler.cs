using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Holidays.UpdateHoliday
{
    public class UpdateHolidayCommandHandler : IRequestHandler<UpdateHolidayCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateHolidayCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateHolidayCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Holidays.FirstAsync(h => h.Id == request.Data.Id);

            _mapper.Map(request.Data, item);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}