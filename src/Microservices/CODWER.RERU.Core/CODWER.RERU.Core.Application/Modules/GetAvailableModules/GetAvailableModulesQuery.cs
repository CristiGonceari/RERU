using System.Collections.Generic;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.GetModules {
    public class GetAvailableModulesQuery : IRequest<IEnumerable<ModuleDto>> { }
}