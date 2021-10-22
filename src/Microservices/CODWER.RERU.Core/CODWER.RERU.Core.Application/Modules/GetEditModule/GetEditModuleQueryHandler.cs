using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Data.Persistence.Context;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Modules.GetEditModule {
    public class GetEditModuleQueryHandler : BaseHandler, IRequestHandler<GetEditModuleQuery, ModuleDto> {
        public GetEditModuleQueryHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }

        public async Task<ModuleDto> Handle (GetEditModuleQuery request, CancellationToken cancellationToken) {
            var module = await CoreDbContext.Modules.FirstOrDefaultAsync (m => m.Id == request.Id);
            return Mapper.Map<ModuleDto> (module);
        }
    }
}