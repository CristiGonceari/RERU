using System;
using CODWER.RERU.Personal.Data.Entities.Files;

namespace CODWER.RERU.Personal.DataTransferObjects.Reports
{
    public class ReportItemDto
    {
        public int Id { get; set; }
        public FileTypeEnum Type { get; set; }
        public string Name { get; set; }
        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
        public string ContractorLastName { get; set; }
        public string ContractorFatherName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
