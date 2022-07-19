using System;
using System.Linq;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using CODWER.RERU.Personal.DataTransferObjects.Enums;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorMappings
{
    public class EmployerStateConverter : IValueConverter<Contractor, string>
    {
        public string Convert(Contractor sourceMember, ResolutionContext context)
        {
            var now = DateTime.Now;

            return sourceMember.Positions.Any(p =>
                (p.FromDate == null && p.ToDate == null)
                || (p.ToDate == null && p.FromDate != null && p.FromDate < now)
                || (p.FromDate == null && p.ToDate != null && p.ToDate > now)
                || (p.FromDate != null && p.ToDate != null && p.FromDate < now && p.ToDate > now))

                ? EmployerStateEnum.InService.ToString()
                : EmployerStateEnum.Dismissed.ToString();
        }
    }
}
