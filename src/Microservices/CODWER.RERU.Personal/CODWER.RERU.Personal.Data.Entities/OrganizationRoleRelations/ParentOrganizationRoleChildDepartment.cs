namespace CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations
{
    public class ParentOrganizationRoleChildDepartment : DepartmentRoleRelation
    {
        public int? ParentOrganizationRoleId { get; set; }
        public OrganizationRole ParentOrganizationRole { get; set; }

        public int ChildDepartmentId { get; set; }
        public Department ChildDepartment { get; set; }
    }
}
