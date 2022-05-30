using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.PrintCandidatePosition
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_POZITII_CANDIDATULUI)]
    public class PrintCandidatePositionCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
    }
}
