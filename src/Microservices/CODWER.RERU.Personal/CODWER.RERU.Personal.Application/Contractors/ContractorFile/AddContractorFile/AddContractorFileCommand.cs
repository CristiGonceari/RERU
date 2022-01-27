using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.AddContractorFile
{
    [ModuleOperation(permission: PermissionCodes.CONTRACTOR_FILE_GENERAL_ACCESS)]
    public class AddContractorFileCommand : IRequest<int>
    {
        public AddFileDto Data { get; set; }
    }
}
