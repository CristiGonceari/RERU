using System;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Personal.Application.Reports.PrintReports
{
    public class PrintReportsCommand : IRequest<FileDataDto>
    {
        public FileTypeEnum? Type { get; set; }
        public string Name { get; set; }
        public string ContractorName { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
