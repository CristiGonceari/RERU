using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests
{
    public static class GetAndFilterSolicitedTests
    {
        public static IQueryable<SolicitedVacantPosition> Filter(AppDbContext appDbContext, int? positionId, string userName, SolicitedPositionStatusEnum? status, DateTime? fromDate, DateTime? tillDate)
        {
            var solicitedTests = appDbContext.SolicitedVacantPositions
                .Include(t => t.UserProfile)
                .Include(t => t.CandidatePosition)
                .Include(t => t.CandidatePosition.RequiredDocumentPositions)
                .Include(x => x.SolicitedVacantPositionUserFiles)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(userName))
            {
                solicitedTests = solicitedTests.Where(x => x.UserProfile.FirstName.ToLower().Contains(userName.ToLower()) ||
                                                           x.UserProfile.LastName.ToLower().Contains(userName.ToLower()) ||
                                                           x.UserProfile.FatherName.ToLower().Contains(userName.ToLower()) ||
                                                           x.UserProfile.Idnp.Contains(userName));
            }

            if (positionId != null)
            {
                solicitedTests = solicitedTests.Where(x => x.CandidatePositionId == positionId);
            }

            if (status != null)
            {
                solicitedTests = solicitedTests.Where(x => x.SolicitedPositionStatus == status);
            }

            if (fromDate != null)
            {
                solicitedTests = solicitedTests.Where(x => x.CreateDate >= fromDate);
            }

            if (tillDate != null)
            {
                solicitedTests = solicitedTests.Where(x => x.CreateDate <= tillDate);
            }

            return solicitedTests;
        }
    }
}
