using CODWER.RERU.Personal.DataTransferObjects.Reports;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService.Entities;
using MediatR;
using System;

namespace CODWER.RERU.Personal.Application.Reports.GetReports
{
    public class GetReportsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ReportItemDto>>
    {
        public FileTypeEnum? FileType { get; set; }
        public string Name { get; set; }
        public string ContractorName { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
