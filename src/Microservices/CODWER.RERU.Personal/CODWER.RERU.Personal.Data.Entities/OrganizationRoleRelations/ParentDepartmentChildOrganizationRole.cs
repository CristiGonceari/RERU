namespace CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations
{
    public class ParentDepartmentChildOrganizationRole : DepartmentRoleRelation
    {
        public int? ParentDepartmentId { get; set; }
        public Department ParentDepartment { get; set; }

        public int ChildOrganizationRoleId { get; set; }
        public OrganizationRole ChildOrganizationRole { get; set; }
    }
}
