using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Application.Modules.UpdateModulePermissions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Modules.EditModule {
    public class EditModuleCommandHandler : BaseHandler, IRequestHandler<EditModuleCommand, Unit> {
        public EditModuleCommandHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }
        public async Task<Unit> Handle (EditModuleCommand request, CancellationToken cancellationToken) {
            var module = await CoreDbContext.Modules.Include (m => m.Permissions).FirstOrDefaultAsync (m => m.Id == request.Module.Id);
            Mapper.Map (request.Module, module);
            await CoreDbContext.SaveChangesAsync ();

            await Mediator.Send (new UpdateModulePermissionsCommand (module));
            return Unit.Value;
        }
    }
}