using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.GetContractorFile
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_DOCUMENTELE_CONTRACTORILOR)]
    public class GetContractorFileQuery : IRequest<FileDataDto>
    {
        public string FileId { get; set; }
    }
}
