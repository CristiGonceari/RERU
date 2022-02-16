using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService.Entities;
using MediatR;
using System;

namespace CODWER.RERU.Personal.Application.Reports.PrintReports
{
    public class PrintReportsCommand : IRequest<FileDataDto>
    {
        public FileTypeEnum? FileType { get; set; }
        public string Name { get; set; }
        public string ContractorName { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
