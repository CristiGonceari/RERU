using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.TimeSheetTables;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.AddTimeSheetTableValue
{
    public class AddEditTimeSheetTableCommandHandler : IRequestHandler<AddEditTimeSheetTableCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddEditTimeSheetTableCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddEditTimeSheetTableCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.TimeSheetTables
                .FirstOrDefaultAsync(x => x.ContractorId == request.Data.ContractorId && x.Date.Date == request.Data.Date.Date);

            if (item != null)
            {
                _mapper.Map(request.Data, item);
                await _appDbContext.SaveChangesAsync();
                
                return item.Id;
            }

            var newItem = _mapper.Map<TimeSheetTable>(request.Data);

            await _appDbContext.TimeSheetTables.AddAsync(newItem);
            await _appDbContext.SaveChangesAsync();

            return newItem.Id;
            
        }
    }
}
