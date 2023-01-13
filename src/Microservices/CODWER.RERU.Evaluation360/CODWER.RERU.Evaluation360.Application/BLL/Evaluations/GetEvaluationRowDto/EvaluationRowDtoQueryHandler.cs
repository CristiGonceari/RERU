using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using CVU.ERP.Common.Pagination;
using CVU.ERP.ServiceProvider;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Evaluation360;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEvaluationRowDto
{
    public class EvaluationRowDtoQueryHandler : IRequestHandler<EvaluationRowDtoQuery, PaginatedModel<EvaluationRowDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly IPaginationService _paginationService;
        public EvaluationRowDtoQueryHandler(AppDbContext dbContext, ICurrentApplicationUserProvider currentUserProvider, IPaginationService paginationService)
        {
            _dbContext = dbContext;
            _currentUserProvider = currentUserProvider;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<EvaluationRowDto>> Handle(EvaluationRowDtoQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentUserProvider.Get();
            var currentUserId = int.Parse(currentUser.Id);

            var evaluations = _dbContext.Evaluations
                                    .Include(e=> e.EvaluatedUserProfile)
                                    .Include(e=> e.EvaluatorUserProfile)
                                    .Include(e=> e.CounterSignerUserProfile)
                                    .Where(e => e.EvaluatedUserProfileId == currentUserId ||  e.EvaluatorUserProfileId == currentUserId ||  e.CounterSignerUserProfileId == currentUserId)
                                    .OrderByDescending(e => e.CreateDate)
                                    .AsQueryable();

            var ev = _dbContext.Evaluations;
            foreach (var e in ev)
            {
                List<decimal?> listForM1 = new List<decimal?> {e.Question1, e.Question2, e.Question3, e.Question4, e.Question5};
                decimal? m1 = listForM1.Average();

                List<decimal?> listForM2 = new List<decimal?> {e.Question6, e.Question7, e.Question8};
                decimal? m2 = listForM2.Average();

                List<decimal?> listForM3 = new List<decimal?> {e.Score1, e.Score2, e.Score3, e.Score4, e.Score5};
                decimal? m3 = listForM3.Average();

                List<decimal?> listForPb = new List<decimal?> {e.Question9, e.Question10, e.Question11, e.Question12};
                decimal? pb = listForPb.Average();

                List<decimal?> listForM4 = new List<decimal?> {e.Question13, pb};
                decimal? m4 = listForM4.Average();

                List<decimal?> listForMea = new List<decimal?> {m1, m2, m3, m4};
                decimal? mea = listForMea.Average();

                decimal? mf;

                if (e.PartialEvaluationScore != null)
                {
                    List<decimal?> listForMf = new List<decimal?> {mea, e.PartialEvaluationScore};
                    mf = listForMf.Average();
                }
                else
                {
                    mf = mea;
                }

                if (mf >= 1 && mf <= 1.5m) e.FinalEvaluationQualification = QualifierEnum.Dissatisfied;
                else if (mf >= 1.51m && mf <= 2.5m) e.FinalEvaluationQualification = QualifierEnum.Satisfied;
                else if (mf >= 2.51m && mf <= 3.5m) e.FinalEvaluationQualification = QualifierEnum.Good;
                else if (mf >= 3.51m && mf <= 4m) e.FinalEvaluationQualification = QualifierEnum.VeryGood;

                e.Points = mf;
            }

            await _dbContext.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(request.EvaluatedName))
            {
                var toSearch = request.EvaluatedName.Split(' ').ToList();

                foreach (var s in toSearch)
                {
                    evaluations = evaluations.Where(p => p.EvaluatedUserProfile.FirstName.ToLower().Contains(s.ToLower())
                        || p.EvaluatedUserProfile.LastName.ToLower().Contains(s.ToLower())
                        || p.EvaluatedUserProfile.FatherName.ToLower().Contains(s.ToLower()));
                }
            }

            if (!string.IsNullOrWhiteSpace(request.EvaluatorName))
            {
                var toSearch = request.EvaluatorName.Split(' ').ToList();

                foreach (var s in toSearch)
                {
                    evaluations = evaluations.Where(p => p.EvaluatorUserProfile.FirstName.ToLower().Contains(s.ToLower())
                        || p.EvaluatorUserProfile.LastName.ToLower().Contains(s.ToLower())
                        || p.EvaluatorUserProfile.FatherName.ToLower().Contains(s.ToLower()));
                }
            }

            if (!string.IsNullOrWhiteSpace(request.CounterSignerName))
            {
                var toSearch = request.CounterSignerName.Split(' ').ToList();

                foreach (var s in toSearch)
                {
                    evaluations = evaluations.Where(p => p.CounterSignerUserProfile.FirstName.ToLower().Contains(s.ToLower())
                        || p.CounterSignerUserProfile.LastName.ToLower().Contains(s.ToLower())
                        || p.CounterSignerUserProfile.FatherName.ToLower().Contains(s.ToLower()));
                }
            }

            if (request.Type.HasValue)
            {
                evaluations = evaluations.Where(x => (int)x.Type == request.Type);
            }

            if (request.Status.HasValue)
            {
                evaluations = evaluations.Where(x => (int)x.Status == request.Status);
            }

            if (request.CreateDateFrom.HasValue)
            {
                evaluations = evaluations.Where(x => x.CreateDate.Date >= request.CreateDateFrom.Value.Date);
            }

            if (request.CreateDateTo.HasValue )
            {
                evaluations = evaluations.Where(x => x.CreateDate.Date <= request.CreateDateTo.Value.Date);
            }
        
            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Evaluation, EvaluationRowDto>(evaluations, request);
            foreach(var e in  paginatedModel.Items)
            {
                e.canAccept = e.canCounterSign = e.canFinished = e.canEvaluate = e.canDelete = e.canDownload = false;

                if (currentUserId == e.EvaluatorUserProfileId && e.Status == 1) 
                {
                    e.canEvaluate = e.canDelete = true;
                }

                if (currentUserId == e.EvaluatedUserProfileId && e.Status == 2)
                { 
                    e.canAccept = true;
                }

                if (currentUserId == e.CounterSignerUserProfileId && e.Status == 3)
                { 
                    e.canCounterSign = true;
                }

                if (currentUserId == e.EvaluatorUserProfileId && e.Status == 4)
                { 
                    e.canEvaluate = true;
                }

                if (currentUserId == e.EvaluatedUserProfileId && e.Status == 5)
                { 
                    e.canFinished = true;
                }

                if (currentUserId == e.EvaluatorUserProfileId && e.Status == 6)
                { 
                    e.canEvaluate = true;
                }
                
                if ((currentUserId == e.EvaluatorUserProfileId || currentUserId == e.CounterSignerUserProfileId) && e.Status == 7)
                { 
                    e.canDownload = true;
                }
            }
            return paginatedModel;
        }
    }
}