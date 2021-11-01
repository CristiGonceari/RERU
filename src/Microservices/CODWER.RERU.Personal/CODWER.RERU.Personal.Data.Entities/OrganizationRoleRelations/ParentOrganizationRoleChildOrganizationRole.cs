namespace CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations
{
    public class ParentOrganizationRoleChildOrganizationRole : DepartmentRoleRelation
    {
        public int? ParentOrganizationRoleId { get; set; }
        public OrganizationRole ParentOrganizationRole { get; set; }

        public int ChildOrganizationRoleId { get; set; }
        public OrganizationRole ChildOrganizationRole { get; set; }
    }
}
