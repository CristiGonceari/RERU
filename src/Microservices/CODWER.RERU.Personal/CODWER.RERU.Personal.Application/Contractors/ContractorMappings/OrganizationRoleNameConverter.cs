using System;
using System.Linq;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorMappings
{
    public class OrganizationRoleConverter : IValueConverter<Contractor, string>
    {
        public string Convert(Contractor contractor, ResolutionContext context)
        {
            var currentPosition = contractor.GetCurrentPositionOnData(DateTime.Now);

            return currentPosition != null
                ? currentPosition.OrganizationRole?.Name
                : GetLastOrganizationRole(contractor);
        }

        private string GetLastOrganizationRole(Contractor sourceMember)
        {
            var now = DateTime.Now;

            var lastPosition = sourceMember.Positions
                .Where(p => p.ToDate < now)
                .OrderByDescending(p => p.ToDate)
                .FirstOrDefault();

            return lastPosition != null
                ? lastPosition.OrganizationRole?.Name
                : "-";
        }
    }
}
