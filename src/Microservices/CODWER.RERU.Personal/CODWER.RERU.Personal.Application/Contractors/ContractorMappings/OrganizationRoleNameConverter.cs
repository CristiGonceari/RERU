using System;
using System.Linq;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorMappings
{
    public class RoleConverter : IValueConverter<Contractor, string>
    {
        public string Convert(Contractor contractor, ResolutionContext context)
        {
            var currentPosition = contractor.GetCurrentPositionOnData(DateTime.Now);

            return currentPosition != null
                ? currentPosition.Role?.Name
                : GetLastRole(contractor);
        }

        private string GetLastRole(Contractor sourceMember)
        {
            var now = DateTime.Now;

            var lastPosition = sourceMember.Positions
                .Where(p => p.ToDate < now)
                .OrderByDescending(p => p.ToDate)
                .FirstOrDefault();

            return lastPosition != null
                ? lastPosition.Role?.Name
                : "-";
        }
    }
}
