using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.PrintUserProfileModules
{
    public class PrintUserProfileModulesCommand : TableParameter, IRequest<FileDataDto>
    {
    }
}
