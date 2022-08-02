namespace CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents
{
    public class AddEditDepartmentRoleContentDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public int RoleCount { get; set; }
    }
}
