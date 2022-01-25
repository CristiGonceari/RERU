using System;
using System.Linq;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorMappings
{
    public class DepartmentNameConverter : IValueConverter<Contractor, string>
    {
        public string Convert(Contractor contractor, ResolutionContext context)
        {
            var currentPosition = contractor.GetCurrentPositionOnData(DateTime.Now);

            return currentPosition != null
                ? currentPosition.Department?.Name
                : GetLastDepartmentName(contractor);
        }

        private string GetLastDepartmentName(Contractor sourceMember)
        {
            var now = DateTime.Now;

            var lastPosition = sourceMember.Positions
                .Where(p => p.ToDate < now)
                .OrderByDescending(p => p.ToDate)
                .FirstOrDefault();

            return lastPosition != null
                ? lastPosition.Department?.Name
                : "-";
        }
    }
}
