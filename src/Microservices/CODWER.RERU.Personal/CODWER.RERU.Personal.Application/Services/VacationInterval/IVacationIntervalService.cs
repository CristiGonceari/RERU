using System;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services.VacationInterval
{
    public interface IVacationIntervalService
    {
        Task<double> GetContractorAvailableDays(int contractorId);
        Task<int> GetVacationDaysByInterval(DateTime from, DateTime? to);
        Task<double> GetCalculatedContractorAvailableDays(int contractorId, int VacantionTypeId);
    }
}