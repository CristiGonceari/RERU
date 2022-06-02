using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.PrintDocumentTemplates
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_POZITII_CANDIDATULUI)]
    public class PrintDocumentTemplatesCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
        public FileTypeEnum FileType { get; set; }
    }
}
