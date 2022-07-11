using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.PrintRequiredDocuments
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_SABLOANELE_DE_DOCUMENTE)]
    public class PrintRequiredDocumentsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
        public bool? Mandatory { get; set; }
    }
}
