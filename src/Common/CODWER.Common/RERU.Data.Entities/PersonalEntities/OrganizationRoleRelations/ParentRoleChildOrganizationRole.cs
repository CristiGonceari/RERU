namespace RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations
{
    public class ParentRoleChildRole : DepartmentRoleRelation
    {
        public int? ParentRoleId { get; set; }
        public Role ParentRole { get; set; }

        public int ChildRoleId { get; set; }
        public Role ChildRole { get; set; }
    }
}
