using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.StorageService.Models;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.AddContractorFile
{
    [ModuleOperation(permission: PermissionCodes.CONTRACTOR_FILE_GENERAL_ACCESS)]
    public class AddContractorFileCommand : IRequest<string>
    {
        public int ContractorId { get; set; }
        public AddFileDto Data { get; set; }
    }
}
