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
                                    .Where(e => e.EvaluatedUserProfileId == currentUserId ||  
                                                e.EvaluatorUserProfileId == currentUserId ||  
                                                e.CounterSignerUserProfileId == currentUserId)
                                    .OrderByDescending(e => e.CreateDate)
                                    .AsQueryable();

            CalculatePoints(evaluations);

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

            await _dbContext.SaveChangesAsync();

            SetPermissions(paginatedModel, currentUserId);

            return paginatedModel;
        }

        private void CalculatePoints(IQueryable<Evaluation> evaluations)
        {
            decimal? m1 =  0, m2 = 0, m3 = 0, m4 = 0, pb = 0, mea = 0, mf = 0;

            foreach (var evaluation in evaluations)
            {
                List<decimal?> listForM1 = new List<decimal?> {evaluation.Question1, evaluation.Question2, evaluation.Question3, evaluation.Question4, evaluation.Question5};
                m1 = listForM1.Average();

                List<decimal?> listForM2 = new List<decimal?> {evaluation.Question6, evaluation.Question7, evaluation.Question8};
                m2 = listForM2.Average();

                if (evaluation.Score1 == null) evaluation.Score1 = 0;
                if (evaluation.Score2 == null) evaluation.Score2 = 0;
                if (evaluation.Score3 == null) evaluation.Score3 = 0;
                if (evaluation.Score4 == null) evaluation.Score4 = 0;
                if (evaluation.Score5 == null) evaluation.Score5 = 0;

                List<decimal?> listForM3 = new List<decimal?> {evaluation.Score1, evaluation.Score2, evaluation.Score3, evaluation.Score4, evaluation.Score5};
                m3 = listForM3.Average();

                List<decimal?> listForPb = new List<decimal?> {evaluation.Question9, evaluation.Question10, evaluation.Question11, evaluation.Question12};
                pb = listForPb.Average();

                List<decimal?> listForM4 = new List<decimal?> {evaluation.Question13, pb};
                m4 = listForM4.Average();

                List<decimal?> listForMea = new List<decimal?> {m1, m2, m3, m4};
                mea = listForMea.Average();

                if (evaluation.PartialEvaluationScore != null)
                {
                    List<decimal?> listForMf = new List<decimal?> {mea, evaluation.PartialEvaluationScore};
                    mf = listForMf.Average();
                }
                else
                {
                    mf = mea;
                }

                Console.WriteLine("score1 = " + evaluation.Score1 + " m1 = " + m1 + " m2 = " + m2 + " m3 = " + m3 + " m4 = " + m4 + " pb = " + pb + " mea = " + mea);

                if (mf != null) evaluation.Points = Math.Round(mf.Value, 2);
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