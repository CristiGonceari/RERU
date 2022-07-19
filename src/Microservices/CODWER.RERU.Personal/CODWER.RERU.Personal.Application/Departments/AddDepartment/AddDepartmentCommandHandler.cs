using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;

namespace CODWER.RERU.Personal.Application.Departments.AddDepartment
{
    public class AddDepartmentCommandHandler : IRequestHandler<AddDepartmentCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddDepartmentCommand> _loggerService;

        public AddDepartmentCommandHandler(
                AppDbContext appDbContext, 
                IMapper mapper,
                ILoggerService<AddDepartmentCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var toAdd = _mapper.Map<Department>(request.Data);

            await _appDbContext.Departments.AddAsync(toAdd);
            await _appDbContext.SaveChangesAsync();

            await LogAction(toAdd);

            return toAdd.Id;
        }

        private async Task LogAction(Department department)
        {
            await _loggerService.Log(LogData.AsPersonal($"{department.Name} was added to Departments list", department));
        }
    }
}
