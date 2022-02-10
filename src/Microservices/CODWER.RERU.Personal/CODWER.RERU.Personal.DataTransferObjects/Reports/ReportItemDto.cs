using CVU.ERP.StorageService.Entities;
using System;

namespace CODWER.RERU.Personal.DataTransferObjects.Reports
{
    public class ReportItemDto
    {
        public string Id { get; set; }
        public FileTypeEnum Type { get; set; }
        public string FileName { get; set; }
        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
        public string ContractorLastName { get; set; }
        public string ContractorFatherName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
