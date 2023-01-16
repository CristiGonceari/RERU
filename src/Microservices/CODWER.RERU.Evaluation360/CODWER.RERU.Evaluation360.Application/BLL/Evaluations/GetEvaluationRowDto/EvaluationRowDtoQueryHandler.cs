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
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.Evaluation360;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEvaluationRowDto
{
    public class EvaluationRowDtoQueryHandler : IRequestHandler<EvaluationRowDtoQuery, PaginatedModel<EvaluationRowDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly IPaginationService _paginationService;
        public EvaluationRowDtoQueryHandler(
            AppDbContext dbContext, 
            ICurrentApplicationUserProvider currentUserProvider, 
            IPaginationService paginationService)
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

            CalculatePoints(evaluations);

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

            if (request.CreateDateTo.HasValue)
            {
                evaluations = evaluations.Where(x => x.CreateDate.Date <= request.CreateDateTo.Value.Date);
            }
        
            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Evaluation, EvaluationRowDto>(evaluations, request);
            
            SetPermissions(paginatedModel, currentUserId);

            return paginatedModel;
        }

        private QualifiersEnum GetQualification(decimal? mf)
        {
            if (mf >= 1m && mf <= 1.5m) return QualifiersEnum.Dissatisfied;
            else if (mf >= 1.51m && mf <= 2.5m) return QualifiersEnum.Satisfied;
            else if (mf >= 2.51m && mf <= 3.5m) return QualifiersEnum.Good;
            else if (mf >= 3.51m && mf <= 4m) return QualifiersEnum.VeryGood;
            else throw new ArgumentOutOfRangeException("mf", "Value not within valid range for qualification.");
        }

        private void CalculatePoints(IQueryable<Evaluation> evaluations)
        {
            foreach (var evaluation in evaluations)
            {
                List<decimal?> listForM1 = new List<decimal?> {evaluation.Question1, evaluation.Question2, evaluation.Question3, evaluation.Question4, evaluation.Question5};
                decimal? m1 = listForM1.Average();

                List<decimal?> listForM2 = new List<decimal?> {evaluation.Question6, evaluation.Question7, evaluation.Question8};
                decimal? m2 = listForM2.Average();

                List<decimal?> listForM3 = new List<decimal?> {evaluation.Score1, evaluation.Score2, evaluation.Score3, evaluation.Score4, evaluation.Score5};
                decimal? m3 = listForM3.Average();

                List<decimal?> listForPb = new List<decimal?> {evaluation.Question9, evaluation.Question10, evaluation.Question11, evaluation.Question12};
                decimal? pb = listForPb.Average();

                List<decimal?> listForM4 = new List<decimal?> {evaluation.Question13, pb};
                decimal? m4 = listForM4.Average();

                List<decimal?> listForMea = new List<decimal?> {m1, m2, m3, m4};
                decimal? mea = listForMea.Average();

                decimal? mf = 0;

                if (evaluation.PartialEvaluationScore != null)
                {
                    List<decimal?> listForMf = new List<decimal?> {mea, evaluation.PartialEvaluationScore};
                    mf = listForMf.Average();
                }
                else
                {
                    mf = mea;
                }

                if (mf != null) evaluation.FinalEvaluationQualification = GetQualification(mf);
                evaluation.Points = mf;
            }
        }

        private void SetPermissions(PaginatedModel<EvaluationRowDto> paginatedModel, int currentUserId)
        {
            foreach(var evaluation in paginatedModel.Items)
            {
                evaluation.canAccept = evaluation.canCounterSign = evaluation.canFinished = evaluation.canEvaluate = evaluation.canDelete = evaluation.canDownload = false;

                if (currentUserId == evaluation.EvaluatorUserProfileId && evaluation.Status == 1) 
                {
                    evaluation.canEvaluate = evaluation.canDelete = true;
                }

                if (currentUserId == evaluation.EvaluatedUserProfileId && evaluation.Status == 2)
                {
                    evaluation.canAccept = true;
                }

                if (currentUserId == evaluation.CounterSignerUserProfileId && evaluation.Status == 3)
                {   
                    evaluation.canCounterSign = true;
                }

                if (currentUserId == evaluation.EvaluatorUserProfileId && evaluation.Status == 4)
                {    
                    evaluation.canEvaluate = true;
                }

                if (currentUserId == evaluation.EvaluatedUserProfileId && evaluation.Status == 5)
                {   
                    evaluation.canFinished = true;
                }

                if (currentUserId == evaluation.EvaluatorUserProfileId && evaluation.Status == 6)
                {    
                    evaluation.canEvaluate = true;
                }
                
                if ((currentUserId == evaluation.EvaluatorUserProfileId || currentUserId == evaluation.CounterSignerUserProfileId) && evaluation.Status == 7)
                {   
                    evaluation.canDownload = true;
                }
            }
        }
    }
}