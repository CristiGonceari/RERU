using System.Collections.Generic;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions
{
    public static class GetAndPrintCandidatePosition
    {
        public static IQueryable<CandidatePosition> Filter(AppDbContext appDbContext, PositionFiltersDto data)
        {
            var positions = appDbContext.CandidatePositions
                .Include(x => x.EventUsers)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(data.Name))
            {
                positions = positions.Where(x => x.Name.ToLower().Contains(data.Name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(data.ResponsiblePersonName))
            {
                positions = positions.Where(x => GetResponsibleUserIds(appDbContext, data.ResponsiblePersonName).Contains(x.CreateById));
            }

            if (data.MedicalColumn.HasValue)
            {
                positions = positions.Where(x => x.MedicalColumn == data.MedicalColumn);
            }

            if (data.ActiveFrom.HasValue)
            {
                positions = positions.Where(x => x.From >= data.ActiveFrom);
            }

            if (data.ActiveTo.HasValue)
            {
                positions = positions.Where(x => x.To <= data.ActiveTo);
            }

            return positions;
        }

        private static List<string> GetResponsibleUserIds(AppDbContext appDbContext, string userName)
        {
            var userProfiles = appDbContext.UserProfiles.AsQueryable();

            var toSearch = userName.Split(' ').ToList();

            foreach (var s in toSearch)
            {
                userProfiles = userProfiles.Where(p =>
                    p.FirstName.ToLower().Contains(s.ToLower())
                    || p.LastName.ToLower().Contains(s.ToLower())
                    || p.FatherName.ToLower().Contains(s.ToLower()));
            }

            return userProfiles.Select(x => x.Id.ToString()).ToList();
        }
    }
}
