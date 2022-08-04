using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.AddEditRequiredDocument
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_DOCUMENTE_NECESARE)]
    public class AddEditRequiredDocumentsCommand : IRequest<int>
    {
        public AddEditRequiredDocumentsDto Data { get; set; }
    }
}
