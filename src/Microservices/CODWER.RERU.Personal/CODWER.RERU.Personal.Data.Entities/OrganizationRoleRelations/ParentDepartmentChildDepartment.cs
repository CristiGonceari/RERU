namespace CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations
{
    public class ParentDepartmentChildDepartment : DepartmentRoleRelation
    {
        public int? ParentDepartmentId { get; set; }
        public Department ParentDepartment { get; set; }

        public int ChildDepartmentId { get; set; }
        public Department ChildDepartment { get; set; }
    }
}
