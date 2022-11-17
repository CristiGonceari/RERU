using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.EditTestTemplate
{
    public class EditTestTemplateCommandHandler : IRequestHandler<EditTestTemplateCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<EditTestTemplateCommandHandler> _loggerService;
        private readonly IAssignRoleService _assignRoleService;

        public EditTestTemplateCommandHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            ILoggerService<EditTestTemplateCommandHandler> logger, 
            IAssignRoleService assignRoleService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = logger;
            _assignRoleService = assignRoleService;
        }

        public async Task<int> Handle(EditTestTemplateCommand request, CancellationToken cancellationToken)
        {
            var updateTestTemplate = await _appDbContext.TestTemplates.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, updateTestTemplate);
            await _appDbContext.SaveChangesAsync();

            await _assignRoleService.AssignRolesToTestTemplate(request.Data.ModuleRoles, updateTestTemplate.Id);

            await LogAction(updateTestTemplate);

            return updateTestTemplate.Id;
        }

        private async Task LogAction(TestTemplate testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($@"Șablonul de test ""{testTemplate.Name}"" a fost actualizat în sistem", testTemplate));
        }
    }
}
