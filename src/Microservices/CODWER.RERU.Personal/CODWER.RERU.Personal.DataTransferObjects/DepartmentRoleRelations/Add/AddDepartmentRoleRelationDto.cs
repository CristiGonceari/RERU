namespace CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Add
{
    public class AddDepartmentRoleRelationDto
    {
        public int ParentId { get; set; }
        public int ChildId { get; set; }

        public DepartmentRoleRelationTypeEnum? RelationType { get; set; }

        public int OrganizationalChartId { get; set; }
    }
}
