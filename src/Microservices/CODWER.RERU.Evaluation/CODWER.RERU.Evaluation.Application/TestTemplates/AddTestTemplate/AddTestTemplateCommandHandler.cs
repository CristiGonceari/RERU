using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.AddTestTemplate
{
    public class AddTestTemplateCommandHandler : IRequestHandler<AddTestTemplateCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddTestTemplateCommandHandler> _loggerService;
        private readonly IAssignRoleService _assignRoleService;

        public AddTestTemplateCommandHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            ILoggerService<AddTestTemplateCommandHandler> loggerService, 
            IAssignRoleService assignRoleService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
            _assignRoleService = assignRoleService;
        }

        public async Task<int> Handle(AddTestTemplateCommand request, CancellationToken cancellationToken)
        {
            var newTestTemplate = _mapper.Map<TestTemplate>(request.Data);

            _appDbContext.TestTemplates.Add(newTestTemplate);

            await _appDbContext.SaveChangesAsync();

            await _assignRoleService.AssignRolesToTestTemplate(request.Data.ModuleRoles, newTestTemplate.Id);

            await LogAction(newTestTemplate);

            return newTestTemplate.Id;
        }

        private async Task LogAction(TestTemplate testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($@"Șablonul de test ""{testTemplate.Name}"" a fost adăugat în sistem", testTemplate));
        }
    }
}
