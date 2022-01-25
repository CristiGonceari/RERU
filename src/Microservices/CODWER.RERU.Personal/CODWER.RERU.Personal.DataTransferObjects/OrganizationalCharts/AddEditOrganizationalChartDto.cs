using System;

namespace CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts
{
    public class AddEditOrganizationalChartDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
    }
}
