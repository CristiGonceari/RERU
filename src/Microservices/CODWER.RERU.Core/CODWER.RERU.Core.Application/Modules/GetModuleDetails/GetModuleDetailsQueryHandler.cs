using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Modules.GetModuleDetails {
    public class GetModuleDetailsQueryHandler : BaseHandler, IRequestHandler<GetModuleDetailsQuery, ModuleDto> {
        public GetModuleDetailsQueryHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }
        public async Task<ModuleDto> Handle (GetModuleDetailsQuery request, CancellationToken cancellationToken) {
            var module = await CoreDbContext.Modules.FirstOrDefaultAsync (m => m.Id == request.Id);
            return Mapper.Map<ModuleDto> (module);
        }
    }
}