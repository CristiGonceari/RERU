using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.Configurations;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Services.VacationInterval
{
    public class VacationIntervalService : IVacationIntervalService
    {
        private readonly VacationConfiguration _configuration;
        private readonly AppDbContext _appDbContext;
        private List<Holiday> _holidays;

        public VacationIntervalService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _configuration = appDbContext.VacationConfigurations.FirstOrDefault() ??
            new VacationConfiguration
            {
                PaidLeaveDays = 28,
                NonPaidLeaveDays = 60,
                StudyLeaveDays = 62,
                DeathLeaveDays = 3,
                ChildCareLeaveDays = 792,
                ChildBirthLeaveDays = 126,
                MarriageLeaveDays = 3,
                PaternalistLeaveDays = 14,
                IncludeOffDays = true,
                IncludeHolidayDays = true,

                MondayIsWorkDay = true,
                TuesdayIsWorkDay = true,
                WednesdayIsWorkDay = true,
                ThursdayIsWorkDay = true,
                FridayIsWorkDay = true,
                SaturdayIsWorkDay = false,
                SundayIsWorkDay = false
            };
        }

        public async Task<int> GetVacationDaysByInterval(DateTime from, DateTime? to)
        {
            if (to == null)
            {
                return 1;
            }

            return CalculateDays(from.Date, to.Value.Date);
        }

        public async Task<double> GetContractorAvailableDays(int contractorId)
        {
            var contractor = await _appDbContext.Contractors
                .Include(x => x.Positions)
                .Include(x => x.Contracts)
                .Include(x => x.Vacations)
                .FirstAsync(x => x.Id == contractorId);

            var currentPosition = contractor.GetCurrentPositionOnData(DateTime.Now);
            var contract = contractor.Contracts.LastOrDefault();

            if (currentPosition?.FromDate == null)
            {
                throw new Exception(ValidationCodes.POSITION_NOT_FOUND);
            }

            if (contract == null)
            {
                throw new Exception(ValidationCodes.CONTRACT_NOT_FOUND);
            }

            var contractorTotalVacationDays = (DateTime.Now - currentPosition.FromDate).Value.Days
                                              * contract.VacationDays
                                              / 356;

            var contractorUsedDays = contractor.Vacations
                .Where(x => x.Status == StageStatusEnum.Approved)
                .Select(x => x.CountDays).Sum();

            return contractorTotalVacationDays - contractorUsedDays;
        }

        public async Task<double> GetCalculatedContractorAvailableDays(int contractorId, int VacantionTypeId)
        {
            var contractor = await _appDbContext.Contractors
                .Include(x => x.Positions)
                .Include(x => x.Contracts)
                .Include(x => x.Vacations)
                .FirstAsync(x => x.Id == contractorId);

            var currentPosition = contractor.GetCurrentPositionOnData(DateTime.Now);
            var contract = contractor.Contracts.LastOrDefault();

            if (currentPosition?.FromDate == null)
            {
                throw new Exception(ValidationCodes.POSITION_NOT_FOUND);
            }

            switch (VacantionTypeId)
            {
                case (int)VacationType.BirthOfTheChild:

                    return await CalculateDaysByConfigurations(currentPosition, contractor, _configuration.ChildBirthLeaveDays, VacationType.BirthOfTheChild);

                case (int)VacationType.Studies:
                    
                    return await CalculateDaysByConfigurations(currentPosition, contractor, _configuration.StudyLeaveDays, VacationType.Studies);

                case (int)VacationType.Death:
                   
                    return await CalculateDaysByConfigurations(currentPosition, contractor, _configuration.DeathLeaveDays, VacationType.Death);

                case (int)VacationType.ChildCare:
                   
                    return await CalculateDaysByConfigurations(currentPosition, contractor, _configuration.ChildCareLeaveDays, VacationType.ChildCare);

                case (int)VacationType.Marriage:
                   
                    return await CalculateDaysByConfigurations(currentPosition, contractor, _configuration.MarriageLeaveDays, VacationType.Marriage);

                case (int)VacationType.Paternal:
                   
                    return await CalculateDaysByConfigurations(currentPosition, contractor, _configuration.PaternalistLeaveDays, VacationType.Paternal);

                case (int)VacationType.OwnVacation:
                    
                    return await CalculateDaysByConfigurations(currentPosition, contractor, _configuration.NonPaidLeaveDays, VacationType.OwnVacation);

                case (int)VacationType.PaidAnnual:
                    
                    return await CalculateDaysByConfigurations(currentPosition, contractor, contract.VacationDays, VacationType.PaidAnnual);

            }
            var contractorTotalVacationDays = (DateTime.Now - currentPosition.FromDate).Value.Days
                                                                  * contract.VacationDays
                                                                  / 356;

            var contractorUsedDays = contractor.Vacations
                .Where(x => x.Status == StageStatusEnum.Approved)
                .Select(x => x.CountDays).Sum();

            return contractorTotalVacationDays - contractorUsedDays;
        }

        private async Task<double> CalculateDaysByConfigurations(Position currentPosition, Contractor contractor, int configurationCoeficient, VacationType vacationType)
        {
            var contractorAvailableDays = (DateTime.Now - currentPosition.FromDate).Value.Days
                                       * configurationCoeficient / 356;

            var usedDays = contractor.Vacations
                .Where(x => x.Status == StageStatusEnum.Approved && x.VacationType == vacationType)
                .Select(x => x.CountDays).Sum();

            return contractorAvailableDays - usedDays;
        }

        private int CalculateDays(DateTime from, DateTime to)
        {
            var count = (to - from).Days + 1;
            _holidays = _appDbContext.Holidays.ToList();

            while (from <= to)
            {
                if (_configuration.IncludeHolidayDays && _configuration.IncludeOffDays)
                {
                    count += HolidayAndOffDayCounter(from);
                }
                else if (_configuration.IncludeHolidayDays)
                {
                    count += HolidayCounter(from);
                }
                else if (_configuration.IncludeOffDays)
                {
                    count += OffDayCounter(from);
                }

                from = from.AddDays(1);
            }

            return count;
        }
        private int HolidayAndOffDayCounter(DateTime current)
        {
            var date = current.Date;

            var holidays = HolidayCounter(current);
            var offDay = OffDayCounter(current);

                if (holidays == -1 && offDay == -1)
                {
                    return -1;
                }
                else if (holidays == -1)
                {
                    return -1;
                }
                else if (offDay == -1)
                {
                    return -1;
                }
            return 0;
        }
        private int HolidayCounter(DateTime current)
        {
            var currentDate = current.Date;

            var res = _holidays.Any(h =>
                (h.From.Date == currentDate && h.To == null) ||
                (h.From.Date <= currentDate && h.To != null && currentDate <= h.To.Value.Date));

            return res ? -1 : 0;
        }

        private int OffDayCounter(DateTime current)
        {
            var day = current.DayOfWeek;

            switch (day)
            {
                case DayOfWeek.Monday:
                {
                    if (_configuration.MondayIsWorkDay) return 0;
                    break;
                }
                case DayOfWeek.Tuesday:
                {
                    if (_configuration.TuesdayIsWorkDay) return 0;
                    break;
                }
                case DayOfWeek.Wednesday:
                {
                    if (_configuration.WednesdayIsWorkDay) return 0;
                    break;
                }
                case DayOfWeek.Thursday:
                {
                    if (_configuration.ThursdayIsWorkDay) return 0;
                    break;
                }
                case DayOfWeek.Friday:
                {
                    if (_configuration.FridayIsWorkDay) return 0;
                    break;
                }
                case DayOfWeek.Saturday:
                {
                    if (_configuration.SaturdayIsWorkDay) return 0;
                    break;
                }
                case DayOfWeek.Sunday:
                {
                    if (_configuration.SundayIsWorkDay) return 0;
                    break;
                }
            }

            return -1;
        }
    }
}
