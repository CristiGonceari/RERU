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

            //Console.WriteLine("--------------------------------------------"+currentUser.IsAnonymous);
            // Console.WriteLine("--------------------------------------------"+currentUser.Id);

            //var filterData = GetFilterData(request);
            //var filter = GetAndFilterEvaluations.Filter(_dbContext, filterData, currentUserId);
        
            var evaluations = _dbContext.Evaluations
                                    .Include(e=> e.EvaluatedUserProfile)
                                    .Include(e=> e.EvaluatorUserProfile)
                                    .Include(e=> e.CounterSignerUserProfile)
                                    .Where(e => e.EvaluatedUserProfileId == currentUserId ||  e.EvaluatorUserProfileId == currentUserId ||  e.CounterSignerUserProfileId == currentUserId)
                                    .OrderByDescending(e => e.CreateDate)
                                    .AsQueryable();

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
        
            //var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Evaluation, EvaluationRowDto>(filter, request);
            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Evaluation, EvaluationRowDto>(evaluations, request);
            foreach(var e in  paginatedModel.Items)
            {
                e.canAccept = e.canCounterSign = e.canFinished = e.canEvaluate = e.canDelete = false;

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
            }
            return paginatedModel;
        }

        private EvaluationRowDto GetFilterData(EvaluationRowDtoQuery request) => new EvaluationRowDto
        {
            EvaluatedName = request.EvaluatedName,
            EvaluatorName = request.EvaluatorName,
            CounterSignerName = request.CounterSignerName,
            Type = request.Type,
            Status = request.Status,
            CreateDate = request.CreateDateFrom
        };
    }
}