using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using CODWER.RERU.Personal.Data.Entities.Configurations;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
using CODWER.RERU.Personal.Data.Entities.TimeSheetTables;
using CODWER.RERU.Personal.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.CronJobs
{
    public class JobTimeSheetTable
    {
        private readonly AppDbContext _appDbContext;
        private List<TimeSheetTable> NewValues;
        private List<TimeSheetTable> ExistentTimeSheetTables;
        private List<TimeSheetTable> ExistentWorkedHours;
        private DateTime Now;
        private DateTime Yesterday;

        public JobTimeSheetTable(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            Yesterday = DateTime.Now.AddDays(-1).Date;
            Now = DateTime.Now.AddDays(1).Date;
            NewValues = new List<TimeSheetTable>();
            ExistentTimeSheetTables = _appDbContext.TimeSheetTables.Where(x => x.Date.Date == Now).ToList();
            ExistentWorkedHours = _appDbContext.TimeSheetTables.Where(x => x.Date.Date == Yesterday).ToList();
            
        }
        public void JobForWorkedHours()
        {
            var contractor = _appDbContext.Contractors
                   .Include(c => c.Positions)
                   .InServiceAt(DateTime.Now);

            foreach (var el in contractor)
            {
                var hours = el.Positions.Select(p => p.WorkHours).FirstOrDefault();

                    CheckWorkHours(el.Id, hours);
            }

            _appDbContext.TimeSheetTables.AddRange(NewValues);
            _appDbContext.SaveChanges();

        }
        public void JobForNationalHolidays()
        {
            var contractor = _appDbContext.Contractors
                .Include(x => x.TimeSheetTables)
                .Include(c => c.Vacations)
                .Include(c => c.Positions)
                .InServiceAt(DateTime.Now);

            var list = new List<TimeSheetTable>();
            var oneDay = _appDbContext.TimeSheetTables.Select(x=>x.Value);
            var checkDaysOff = _appDbContext.VacationConfigurations
                .Select(vc => new VacationConfiguration {
                        MondayIsWorkDay = vc.MondayIsWorkDay,
                        TuesdayIsWorkDay = vc.TuesdayIsWorkDay,
                        WednesdayIsWorkDay = vc.WednesdayIsWorkDay,
                        ThursdayIsWorkDay = vc.ThursdayIsWorkDay,
                        FridayIsWorkDay = vc.FridayIsWorkDay,
                        SaturdayIsWorkDay = vc.SaturdayIsWorkDay,
                        SundayIsWorkDay = vc.SundayIsWorkDay
                    });

            foreach (var el in contractor)
            {
                var vacation = el.Vacations.FirstOrDefault(x =>
                    x.FromDate <= Now
                    && x.ToDate >= Now
                    && x.Status == StageStatusEnum.Approved);

                var holiday = _appDbContext.Holidays.FirstOrDefault(x =>
                    x.To == null && x.From.Date == Now || 
                    x.To != null && Now >= x.From && Now <= x.To);

                if (vacation != null)
                {
                    switch (vacation.VacationType)
                    {
                        case VacationType.PaidAnnual:

                            CheckCurrentDay(el.Id, TimeSheetValueEnum.C);
                            break;
                        case VacationType.OwnVacation:

                            CheckCurrentDay(el.Id, TimeSheetValueEnum.Cn);
                            break;

                        case VacationType.Studies:

                            CheckCurrentDay(el.Id, TimeSheetValueEnum.Cs);
                            break;

                        case VacationType.ChildCare:

                            CheckCurrentDay(el.Id, TimeSheetValueEnum.Cc);
                            break;

                        case VacationType.BirthOfTheChild:

                            CheckCurrentDay(el.Id, TimeSheetValueEnum.Cc);
                            break;

                        case VacationType.Death:

                            CheckCurrentDay(el.Id, TimeSheetValueEnum.Dh);
                            break;

                        case VacationType.Marriage:

                            CheckCurrentDay(el.Id, TimeSheetValueEnum.M);
                            break;

                        case VacationType.Paternal:

                            CheckCurrentDay(el.Id, TimeSheetValueEnum.Cc);
                            break;
                    }
                }

                else if (holiday != null)
                {
                    list.Add(new TimeSheetTable()
                    {
                        ContractorId = el.Id,
                        Date = DateTime.Now,
                        Value = TimeSheetValueEnum.Sn
                    });
                }

                else if (checkDaysOff != null)
                {
                    var day = Now.DayOfWeek;

                    switch (day)
                    {
                        case DayOfWeek.Monday:
                            if (checkDaysOff.Any(x => x.MondayIsWorkDay == false))
                            {
                                CheckCurrentDay(el.Id, TimeSheetValueEnum.R);
                            }
                            break;

                        case DayOfWeek.Tuesday:
                            if (checkDaysOff.Any(x => x.TuesdayIsWorkDay == false))
                            {
                                CheckCurrentDay(el.Id, TimeSheetValueEnum.R);
                            }
                            break;

                        case DayOfWeek.Wednesday:
                            if (checkDaysOff.Any(x => x.WednesdayIsWorkDay == false))
                            {
                                CheckCurrentDay(el.Id, TimeSheetValueEnum.R);
                            }
                            break;

                        case DayOfWeek.Thursday:
                            if (checkDaysOff.Any(x => x.ThursdayIsWorkDay == false))
                            {
                                CheckCurrentDay(el.Id, TimeSheetValueEnum.R);
                            }
                            break;

                        case DayOfWeek.Friday:
                            if (checkDaysOff.Any(x => x.FridayIsWorkDay == false))
                            {
                                CheckCurrentDay(el.Id, TimeSheetValueEnum.R);
                            }
                            break;

                        case DayOfWeek.Saturday:
                            if (checkDaysOff.Any(x => x.SaturdayIsWorkDay == false))
                            {
                                CheckCurrentDay(el.Id, TimeSheetValueEnum.R);
                            }
                            break;

                        case DayOfWeek.Sunday:
                            if (checkDaysOff.Any(x => x.SundayIsWorkDay == false))
                            {
                                CheckCurrentDay(el.Id, TimeSheetValueEnum.R);
                            }
                            break;

                    }
                }
               
            }
                 _appDbContext.TimeSheetTables.AddRange(NewValues);
                 _appDbContext.SaveChanges();
        }
        private void CheckCurrentDay(int contractorId, TimeSheetValueEnum value)
        {
            if (!ExistentTimeSheetTables.Any(x => x.ContractorId == contractorId && x.Date == Now))
            {
                NewValues.Add(new TimeSheetTable()
                {
                    ContractorId = contractorId,
                    Date = Now,
                    Value = value
                });
            }
        }
        private void CheckWorkHours(int contractorId, WorkHoursEnum workHours)
        {
            
            if (!ExistentWorkedHours.Any(x => x.ContractorId == contractorId && x.Date == Yesterday))
            {
                NewValues.Add(new TimeSheetTable()
                {
                    ContractorId = contractorId,
                    Date = Yesterday,
                    Value = (TimeSheetValueEnum)(int)workHours
                }); ;
            }   
        }
    }
}
