using System.Collections.Generic;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.GetAvailableModules 
{
    public class GetAvailableModulesQuery : IRequest<IEnumerable<ModuleDto>> { }
}