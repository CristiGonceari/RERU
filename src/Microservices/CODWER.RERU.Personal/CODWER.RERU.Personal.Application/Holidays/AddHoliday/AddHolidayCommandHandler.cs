using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using MediatR;
using RERU.Data.Entities.PersonalEntities.Configurations;

namespace CODWER.RERU.Personal.Application.Holidays.AddHoliday
{
    public class AddHolidayCommandHandler : IRequestHandler<AddHolidayCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddHolidayCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddHolidayCommand request, CancellationToken cancellationToken)
        {
            var mappedItem = _mapper.Map<Holiday>(request.Data);

            await _appDbContext.Holidays.AddAsync(mappedItem);
            await _appDbContext.SaveChangesAsync();

            return mappedItem.Id;
        }
    }
}
