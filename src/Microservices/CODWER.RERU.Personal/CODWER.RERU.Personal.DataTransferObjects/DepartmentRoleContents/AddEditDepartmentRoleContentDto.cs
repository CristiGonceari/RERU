namespace CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents
{
    public class AddEditDepartmentRoleContentDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int OrganizationRoleId { get; set; }
        public int OrganizationRoleCount { get; set; }
    }
}
