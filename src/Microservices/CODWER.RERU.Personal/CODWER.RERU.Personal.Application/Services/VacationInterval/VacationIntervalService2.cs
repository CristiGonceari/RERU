using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Services.VacationInterval
{
    //public class VacationIntervalService : IVacationIntervalService
    //{
    //    private readonly VacationConfiguration _configuration;
    //    private readonly AppDbContext _appDbContext;
    //    private List<Holiday> _holidays;

    //    public VacationIntervalService(AppDbContext appDbContext)
    //    {
    //        _appDbContext = appDbContext;
    //        _configuration = appDbContext.VacationConfigurations.FirstOrDefault() ?? new VacationConfiguration
    //        {
    //            PaidLeaveDays = 28,
    //            NonPaidLeaveDays = 60,
    //            IncludeOffDays = true,
    //            IncludeHolidayDays = true,

    //            MondayIsWorkDay = true,
    //            TuesdayIsWorkDay = true,
    //            WednesdayIsWorkDay = true,
    //            ThursdayIsWorkDay = true,
    //            FridayIsWorkDay = true,
    //            SaturdayIsWorkDay = false,
    //            SundayIsWorkDay = false
    //        };
    //    }

    //    public async Task<double> GetContractorAvailableDays(int contractorId)
    //    {
    //        var contractor = await _appDbContext.Contractors
    //            .Include(c => c.Positions)
    //            .Include(c => c.Vacations)
    //            .FirstOrDefaultAsync(c => c.Id == contractorId);

    //        var firstDay = GetFirstPositionFromDate(contractor);
    //        var lastDay = GetLastPositionToDay(contractor);
    //        lastDay ??= DateTime.Now.Date;

    //        if (firstDay == null)
    //        {
    //            return 0;
    //        }

    //        var contractorVacationsDays = GetContractorVacationSumDays(contractor.Vacations.ToList());

    //        var period = (lastDay - firstDay).Value;
    //        var allPeriodDays = (double)_configuration.PaidLeaveDays / 365 * period.Days;
    //        var count = allPeriodDays - contractorVacationsDays;

    //        return Convert.ToDouble($"{count:0.00}");
    //    }

    //    private DateTime? GetFirstPositionFromDate(Contractor contractor)
    //    {
    //        if (!contractor.Positions.Any())
    //        {
    //            return null;
    //        }
    //        var firstPosition = contractor.Positions.OrderBy(p => p.FromDate).FirstOrDefault();

    //        return firstPosition?.FromDate;
    //    }

    //    private DateTime? GetLastPositionToDay(Contractor contractor)
    //    {
    //        var lastPosition = contractor.Positions.OrderBy(p => p.FromDate).LastOrDefault();

    //        return lastPosition?.ToDate;
    //    }

    //    private int GetContractorVacationSumDays(List<Vacation> contractorVacations)
    //    {
    //        var total = 0;

    //        foreach (var vacation in contractorVacations)
    //        {
    //            _holidays = GetHolidays(vacation.FromDate, vacation.ToDate.Value);

    //            total += CalculateDaysByVacationInterval(vacation.FromDate, vacation.ToDate.Value);
    //        }

    //        return total;
    //    }

    //    private int CalculateDaysByVacationInterval(DateTime from, DateTime to)
    //    {
    //        var count = 0;

    //        if (_configuration.IncludeOffDays && _configuration.IncludeHolidayDays )
    //        {
    //            count = IgnoreOffDaysIgnoreHolidays(from, to);
    //            return count;
    //        }
    //        if (_configuration.IncludeOffDays && !_configuration.IncludeHolidayDays)
    //        {
    //            count = CalculateHolidaysIgnoreOffDays(from, to);

    //            return count;
    //        }
    //        if (!_configuration.IncludeOffDays && _configuration.IncludeHolidayDays)
    //        {
    //            count = CalculateOffDaysIgnoreHolidays(from, to);

    //            return count;
    //        }
    //        if (!_configuration.IncludeOffDays && !_configuration.IncludeHolidayDays)
    //        {
    //            count = CalculateHolidaysIgnoreOffDays(from, to);

    //            return count;
    //        }

    //        return count;
    //    }

    //    ///<summary>
    //    ///Check all intersected holidays with vacation period
    //    ///</summary>
    //    //-----------periodFrom----------------------------------------------periodTo-------------------> vacation period
    //    //-----[case1-case1-case1-case1]----[case2-case2]---------[case3-case3-case3-case3]-------------> holiday cases
    //    //-----[case4-case4-case4-case4-case4-case4-case4-case4-case4-case4-case4-case4-case4]----------> holiday cases
    //    private List<Holiday> GetHolidays(DateTime periodFrom, DateTime periodTo)
    //    {
    //        periodFrom = periodFrom.Date;
    //        periodTo = periodTo.Date;

    //        return _appDbContext.Holidays.Where(h =>
    //                (h.To == null && h.From >= periodFrom && h.From <= periodTo) //check single days holidays
    //                || h.To != null &&                                           //check holiday with interval
    //                (h.From >= periodFrom && h.From <= periodTo //check case1 && case2
    //                 || h.To >= periodFrom && h.To <= periodTo //check case2 && case3
    //                 || h.From <= periodFrom && h.To >= periodTo) //check case4
    //        ).ToList();
    //    }

    //    private int IgnoreOffDaysIgnoreHolidays(DateTime from, DateTime to)
    //    {
    //        var fromDayOfYear = from.DayOfYear;
    //        var toDayOfYear = to.DayOfYear;

    //        return toDayOfYear - fromDayOfYear + 1;
    //    }

    //    private int CalculateHolidaysIgnoreOffDays(DateTime from, DateTime to)  // same algorithm how CalculateOffDaysCalculateHolidays() 
    //    {                                                                                               // because of ignored holiday's counter
    //        var count = 0;

    //        while (from <= to)
    //        {
    //            if (CheckIncludedHoliday(from))
    //            {
    //                //count++;
    //                //ignore holiday days
    //            }
    //            else
    //            {
    //                count += CheckDayBySettings(from);
    //            }

    //            from = from.AddDays(1);
    //        }

    //        return count;
    //    }

    //    private int CalculateOffDaysIgnoreHolidays(DateTime from, DateTime to)
    //    {
    //        var count = 0;

    //        while (from <= to)
    //        {
    //            count += CheckDayBySettings(from);

    //            from = from.AddDays(1);
    //        }

    //        return count;
    //    }

    //    private int CheckDayBySettings(DateTime from)
    //    {
    //        if (!_configuration.IncludeOffDays)
    //        {
    //            return 1;
    //        }

    //        var day = from.DayOfWeek;

    //        switch (day)
    //            {
    //                case DayOfWeek.Monday:
    //                {
    //                    if (_configuration.MondayIsWorkDay) return 1;
    //                    break;
    //                }
    //                case DayOfWeek.Tuesday:
    //                {
    //                    if (_configuration.TuesdayIsWorkDay) return 1;
    //                    break;
    //                }
    //                case DayOfWeek.Wednesday:
    //                {
    //                    if (_configuration.WednesdayIsWorkDay) return 1;
    //                    break;
    //                }
    //                case DayOfWeek.Thursday:
    //                {
    //                    if (_configuration.ThursdayIsWorkDay) return 1;
    //                    break;
    //                }
    //                case DayOfWeek.Friday:
    //                {
    //                    if (_configuration.FridayIsWorkDay) return 1;
    //                    break;
    //                }
    //                case DayOfWeek.Saturday:
    //                {
    //                    if (_configuration.SaturdayIsWorkDay) return 1;
    //                    break;
    //                }
    //                case DayOfWeek.Sunday:
    //                {
    //                    if (_configuration.SundayIsWorkDay) return 1;
    //                    break;
    //                }
    //            }

    //        return 0;
    //    }

    //    private bool CheckIncludedHoliday(DateTime current)
    //    {
    //        var currentDate = current.Date;

    //        var res = _holidays.Any(h =>
    //            (h.From.Date == currentDate && h.To == null) ||
    //            (h.From.Date <= currentDate && h.To != null && currentDate <= h.To.Value.Date));

    //        return res;
    //    }
    //}
}
