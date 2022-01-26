using System;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.PrintTimeSheetTables
{
   public class PrintTimeSheetTableCommand : PaginatedQueryParameter, IRequest<ExportTimeSheetDto>
    {
        public string ContractorName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? DepartmentId { get; set; }
    }
}
