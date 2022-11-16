using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Departments.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<UpdateDepartmentCommand> _loggerService;

        public UpdateDepartmentCommandHandler(
            AppDbContext appDbContext, 
            IMapper mapper,
            ILoggerService<UpdateDepartmentCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Departments.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, item);
            await _appDbContext.SaveChangesAsync();

            await LogAction(item);

            return Unit.Value;
        }

        private async Task LogAction(Department department)
        {
            await _loggerService.Log(LogData.AsPersonal($"Departamentul  {department.Name} a fost actualizat în sistem", department));
        }
    }
}
