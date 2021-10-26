using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Modules.DeleteModule
{
    public class DeleteModuleCommandHandler : BaseHandler, IRequestHandler<DeleteModuleCommand>
    {
        public DeleteModuleCommandHandler(ICommonServiceProvider commonServiceProvider): base(commonServiceProvider){}
        public async Task<Unit> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            var module = await CoreDbContext.Modules.Include(m=>m.Permissions).FirstOrDefaultAsync(m=> m.Id == request.Id);
            CoreDbContext.Modules.Remove(module);
            await CoreDbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}