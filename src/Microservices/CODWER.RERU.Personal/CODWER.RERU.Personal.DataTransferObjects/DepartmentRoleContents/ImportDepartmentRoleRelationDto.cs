using CVU.ERP.Common.DataTransferObjects.Files;

namespace CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents
{
    public class ImportDepartmentRoleRelationDto
    {
        public FileDataDto File { get; set; }
        public bool HasError { get; set; }
    }
}
