using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables;
using CVU.ERP.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RERU.Data.Entities.PersonalEntities.Configurations;
using RERU.Data.Entities.PersonalEntities.TimeSheetTables;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class TimeSheetTableService : ITimeSheetTableService
    {
        private readonly AppDbContext _appDbContext;

        public TimeSheetTableService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> GetFreeHoursForContractor(int contractorId, int workedHours, DateTime from, DateTime to, int totalWorkingDays)
        {
            var position = await _appDbContext.Positions
                    .OrderByDescending(x => x.FromDate)
                    .FirstOrDefaultAsync(x => x.ContractorId == contractorId);

            if (position == null)
            {
                return 0;
            }

            var totalFreeHours = (int)position.WorkHours * totalWorkingDays - workedHours;

            return totalFreeHours;
        }

        public async Task<int> GetWorkedHoursByTimeSheet(List<TimeSheetTable> timeSheetContent)
        {
            var workedHoursList = timeSheetContent
                .Where(x => x?.Value != null && (int)x.Value < 100)
                .Select(x => (int)x.Value)
                .ToList();

            return workedHoursList.Sum();
        }

        public async Task<int> GetWorkedHoursByTimeSheet(List<TimeSheetTableDto> timeSheetContent)
        {
            var workedHoursList = timeSheetContent
                .Where(x => x?.ValueId != null && x.ValueId < 100)
                .Select(x => (int)x.ValueId)
                .ToList();

            return workedHoursList.Sum();
        }

        public async Task<int> GetWorkingDays(DateTime from, DateTime to)
        {
            from = from.Date;
            to = to.Date;
            var totalDays = 0;
            var configuration = GetConfiguration();

            var holidays = _appDbContext.Holidays.Where(x => x.To == null && x.From.Date >= from && x.From.Date <= to
                                                             || x.To != null && x.From.Date >= from && x.To.Value.Date <= to)
                                                            .ToList();

            for (var toDay = from; toDay <= to; toDay = toDay.AddDays(1))
            {
                switch (toDay.DayOfWeek)
                {
                    case DayOfWeek.Monday when configuration.MondayIsWorkDay && TodayItsNotHoliday(holidays, toDay):
                    case DayOfWeek.Tuesday when configuration.TuesdayIsWorkDay && TodayItsNotHoliday(holidays, toDay):
                    case DayOfWeek.Wednesday when configuration.WednesdayIsWorkDay && TodayItsNotHoliday(holidays, toDay):
                    case DayOfWeek.Thursday when configuration.ThursdayIsWorkDay && TodayItsNotHoliday(holidays, toDay):
                    case DayOfWeek.Friday when configuration.FridayIsWorkDay && TodayItsNotHoliday(holidays, toDay):
                    case DayOfWeek.Saturday when configuration.SaturdayIsWorkDay && TodayItsNotHoliday(holidays, toDay):
                    case DayOfWeek.Sunday when configuration.SundayIsWorkDay && TodayItsNotHoliday(holidays, toDay):
                        totalDays++;
                        break;
                }
            }

            return totalDays;
        }

        private bool TodayItsNotHoliday(List<Holiday> holidays, DateTime date)
        {
            return !holidays.Any(x => x.To == null && x.From.Date == date
                                     || x.To != null && x.From.Date <= date && x.To.Value.Date >= date);
        }

        private VacationConfiguration GetConfiguration() => _appDbContext.VacationConfigurations.FirstOrDefault() ??
                                                            new VacationConfiguration
                                                            {
                                                                MondayIsWorkDay = true,
                                                                TuesdayIsWorkDay = true,
                                                                WednesdayIsWorkDay = true,
                                                                ThursdayIsWorkDay = true,
                                                                FridayIsWorkDay = true,
                                                                SaturdayIsWorkDay = false,
                                                                SundayIsWorkDay = false
                                                            };

        public async Task<ExportTimeSheetDto> PrintTimeSheetTableData(PaginatedModel<ContractorTimeSheetTableDto> data, DateTime from, DateTime to)
        {
            using var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");

            var timeSheetValues = data.Items.Select(x => x.Content).ToArray();
            var contractors = data.Items.ToArray();
            var totalColumns = timeSheetValues.First().Count;

            for (var i = 0; i <= contractors.Length; i++)
            {
                if (i == 0)
                {
                    #region ExcelHeader
                    workSheet.Cells[1, 1].Value = "Name";
                    workSheet.Cells[1, 1].Style.Font.Bold = true;
                    SetBordersStyleOnCells(workSheet, 1, 1);

                    for (var j = 0; j < contractors[i].Content.Count; j++)
                    {
                        workSheet.Cells[1, j + 2].Value = timeSheetValues[i][j].Date.ToString("dd");
                        StyleHeaderCells(workSheet, j);
                        SetBordersStyleOnCells(workSheet, 1, j + 2);
                    }

                    workSheet.Cells[1, totalColumns + 2].Value = "Worked Hours";
                    SetBordersStyleOnCells(workSheet, 1, totalColumns + 2);

                    workSheet.Cells[1, totalColumns + 3].Value = "Free Hours";
                    SetBordersStyleOnCells(workSheet, 1, totalColumns + 3);

                    workSheet.Cells[1, totalColumns + 4].Value = "Working Days";
                    SetBordersStyleOnCells(workSheet, 1, totalColumns + 4);

                    StyleBodyCells(workSheet, totalColumns);
                    #endregion
                }
                else
                {
                    #region ExcelBody
                    workSheet.Cells[i + 1, 1].Value = contractors[i - 1].ContractorName;
                    SetBordersStyleOnCells(workSheet, i + 1, 1);

                    for (int j = 0; j < contractors[i - 1].Content.Count; j++)
                    {
                        workSheet.Cells[i + 1, j + 2].Value = timeSheetValues[i - 1][j].Value;
                        workSheet.Cells[i + 1, j + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        SetBordersStyleOnCells(workSheet, i + 1, j + 2);
                    }

                    workSheet.Cells[i + 1, totalColumns + 2].Value = contractors[i - 1].WorkedHours;
                    SetBordersStyleOnCells(workSheet, i + 1, totalColumns + 2);

                    workSheet.Cells[i + 1, totalColumns + 3].Value = contractors[i - 1].FreeHours;
                    SetBordersStyleOnCells(workSheet, i + 1, totalColumns + 3);

                    workSheet.Cells[i + 1, totalColumns + 4].Value = contractors[i - 1].WorkingDays;
                    SetBordersStyleOnCells(workSheet, i + 1, totalColumns + 4);
                    #endregion
                }
            }

            var excelName = $"TimeSheetTable-{from:dd.MM.yyyy}-{to:dd.MM.yyyy}.xlsx";
            var streamBytesArray = package.GetAsByteArray();

            return new ExportTimeSheetDto()
            {
                Content = streamBytesArray,
                Name = excelName,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };

        }

        public async Task<ExportTimeSheetDto> PrintTimeSheetTableData(List<ContractorTimeSheetTableDto> data, DateTime from, DateTime to)
        {
            using var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");

            var timeSheetValues = data.Select(x => x.Content).ToArray();
            var contractors = data.ToArray();
            var totalColumns = timeSheetValues.First().Count;

            for (var i = 0; i <= contractors.Length; i++)
            {
                if (i == 0)
                {
                    #region ExcelHeader
                    workSheet.Cells[1, 1].Value = "Name";
                    workSheet.Cells[1, 1].Style.Font.Bold = true;
                    SetBordersStyleOnCells(workSheet, 1, 1);

                    for (var j = 0; j < contractors[i].Content.Count; j++)
                    {
                        workSheet.Cells[1, j + 2].Value = timeSheetValues[i][j].Date.ToString("dd");
                        StyleHeaderCells(workSheet, j);
                        SetBordersStyleOnCells(workSheet, 1, j + 2);
                    }

                    workSheet.Cells[1, totalColumns + 2].Value = "Worked Hours";
                    SetBordersStyleOnCells(workSheet, 1, totalColumns + 2);

                    workSheet.Cells[1, totalColumns + 3].Value = "Free Hours";
                    SetBordersStyleOnCells(workSheet, 1, totalColumns + 3);

                    workSheet.Cells[1, totalColumns + 4].Value = "Working Days";
                    SetBordersStyleOnCells(workSheet, 1, totalColumns + 4);

                    StyleBodyCells(workSheet, totalColumns);
                    #endregion
                }
                else
                {
                    #region ExcelBody
                    workSheet.Cells[i + 1, 1].Value = contractors[i - 1].ContractorName;
                    SetBordersStyleOnCells(workSheet, i + 1, 1);

                    for (int j = 0; j < contractors[i - 1].Content.Count; j++)
                    {
                        workSheet.Cells[i + 1, j + 2].Value = timeSheetValues[i - 1][j].Value;
                        workSheet.Cells[i + 1, j + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        SetBordersStyleOnCells(workSheet, i + 1, j + 2);
                    }

                    workSheet.Cells[i + 1, totalColumns + 2].Value = contractors[i - 1].WorkedHours;
                    SetBordersStyleOnCells(workSheet, i + 1, totalColumns + 2);

                    workSheet.Cells[i + 1, totalColumns + 3].Value = contractors[i - 1].FreeHours;
                    SetBordersStyleOnCells(workSheet, i + 1, totalColumns + 3);

                    workSheet.Cells[i + 1, totalColumns + 4].Value = contractors[i - 1].WorkingDays;
                    SetBordersStyleOnCells(workSheet, i + 1, totalColumns + 4);
                    #endregion
                }
            }

            var excelName = $"TimeSheetTable-{from:dd.MM.yyyy}-{to:dd.MM.yyyy}.xlsx";
            var streamBytesArray = package.GetAsByteArray();

            return new ExportTimeSheetDto()
            {
                Content = streamBytesArray,
                Name = excelName,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }

        private static void StyleBodyCells(ExcelWorksheet worksheet, int totalColumns)
        {
            worksheet.Column(1).Width = 25;
            worksheet.Cells[1, totalColumns + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column(totalColumns + 2).Width = 15;
            worksheet.Cells[1, totalColumns + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column(totalColumns + 3).Width = 15;
            worksheet.Cells[1, totalColumns + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column(totalColumns + 4).Width = 15;

        }
        private static void StyleHeaderCells(ExcelWorksheet worksheet, int iterator)
        {
            worksheet.Cells[1, iterator + 2].Style.Font.Bold = true;
            worksheet.Cells[1, iterator + 2].Style.Font.Color.SetColor(Color.DarkBlue);
            worksheet.Cells[1, iterator + 2].Style.Font.Name = "Calibri";
            worksheet.Cells[1, iterator + 2].Style.Font.Size = 8;
            worksheet.Cells[1, iterator + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[1, iterator + 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[1, iterator + 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[1, iterator + 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[1, iterator + 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            worksheet.Column(iterator + 2).Width = 4; ;

        }
        private static void SetBordersStyleOnCells(ExcelWorksheet worksheet, int row, int column)
        {
            worksheet.Cells[row, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[row, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[row, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[row, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        
    }
}
