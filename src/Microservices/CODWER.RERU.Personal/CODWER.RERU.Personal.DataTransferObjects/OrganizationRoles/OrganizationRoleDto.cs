namespace CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles
{
    public class RoleDto
    {
        public int Id { get; set; }
        public int ColaboratorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string ShortCode { get; set; }
    }
}
