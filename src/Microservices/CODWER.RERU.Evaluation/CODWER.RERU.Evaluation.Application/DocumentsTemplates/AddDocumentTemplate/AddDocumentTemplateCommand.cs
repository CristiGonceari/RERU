using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.AddDocumentTemplate
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_POZITII_CANDIDATULUI)]
    public class AddDocumentTemplateCommand : IRequest<int>
    {
        public AddEditDocumentTemplateDto Data { get; set; }
    }
}
