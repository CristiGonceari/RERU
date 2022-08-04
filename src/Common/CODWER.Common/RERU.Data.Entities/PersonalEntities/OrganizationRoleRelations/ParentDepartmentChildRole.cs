namespace RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations
{
    public class ParentDepartmentChildRole : DepartmentRoleRelation
    {
        public int? ParentDepartmentId { get; set; }
        public Department ParentDepartment { get; set; }

        public int ChildRoleId { get; set; }
        public Role ChildRole { get; set; }
    }
}
