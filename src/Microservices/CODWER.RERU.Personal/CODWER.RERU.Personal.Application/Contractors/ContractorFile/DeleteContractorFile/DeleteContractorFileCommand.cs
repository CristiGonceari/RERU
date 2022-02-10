using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.DeleteContractorFile
{
    [ModuleOperation(permission: PermissionCodes.CONTRACTOR_FILE_GENERAL_ACCESS)]
    public class DeleteContractorFileCommand : IRequest<Unit>
    {
        public string FileId { get; set; }
    }
}
