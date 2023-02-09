using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles
{
    public class ImportOrganizationalChartDto
    {
        public int OrganizationalChartId { get; set; }
        public IFormFile File { get; set; }
    }
}
