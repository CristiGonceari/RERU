using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.TimeSheetTables;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables;
using CVU.ERP.Common.Pagination;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface ITimeSheetTableService
    {
        public Task<int> GetFreeHoursForContractor(int contractorId, int workedHours, DateTime from, DateTime to, int totalWorkingDays);
        public Task<int> GetWorkedHoursByTimeSheet(List<TimeSheetTable> timeSheetContent);
        public Task<int> GetWorkedHoursByTimeSheet(List<TimeSheetTableDto> timeSheetContent);
        public Task<int> GetWorkingDays(DateTime from, DateTime to);
        public Task<ExportTimeSheetDto> PrintTimeSheetTableData(PaginatedModel<ContractorTimeSheetTableDto> data, DateTime from, DateTime to);
        public Task<ExportTimeSheetDto> PrintTimeSheetTableData(List<ContractorTimeSheetTableDto> data, DateTime from, DateTime to);
    }
}
