using AutoMapper;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities.TimeSheetTables;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.AddTimeSheetTableValue
{
    public class AddEditTimeSheetTableCommandHandler : IRequestHandler<AddEditTimeSheetTableCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddEditTimeSheetTableCommand> _loggerService;

        public AddEditTimeSheetTableCommandHandler(
            AppDbContext appDbContext, 
            IMapper mapper, 
            ILoggerService<AddEditTimeSheetTableCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
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

            await LogAction(newItem);

            return newItem.Id;
            
        }

        private async Task LogAction(TimeSheetTable timeSheetTable)
        {
            await _loggerService.Log(LogData.AsPersonal($"Tabela de pontaj a fost populata cu date pe data de {timeSheetTable.Date:g}", timeSheetTable));
        }
    }
}
