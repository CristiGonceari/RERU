using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.AddContractorFile
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_DOCUMENTELE_CONTRACTORILOR)]
    public class AddContractorFileCommand : IRequest<string>
    {
        public CreateFileDto Data { get; set; }
    }
}
