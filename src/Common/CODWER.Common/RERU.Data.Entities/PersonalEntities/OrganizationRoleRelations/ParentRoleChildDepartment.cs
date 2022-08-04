namespace RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations
{
    public class ParentRoleChildDepartment : DepartmentRoleRelation
    {
        public int? ParentRoleId { get; set; }
        public Role ParentRole { get; set; }

        public int ChildDepartmentId { get; set; }
        public Department ChildDepartment { get; set; }
    }
}
