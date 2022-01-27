using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.GetContractorFile
{
    [ModuleOperation(permission: PermissionCodes.CONTRACTOR_FILE_GENERAL_ACCESS)]
    public class GetContractorFileQuery : IRequest<FileDataDto>
    {
        public int FileId { get; set; }
    }
}
