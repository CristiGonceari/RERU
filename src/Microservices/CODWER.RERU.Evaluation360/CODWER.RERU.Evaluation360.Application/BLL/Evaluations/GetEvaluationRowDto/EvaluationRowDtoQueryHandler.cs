using System;
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

            //Console.WriteLine("--------------------------------------------"+currentUser.IsAnonymous);
            Console.WriteLine("--------------------------------------------"+currentUser.Id);

            var currentUserId = int.Parse(currentUser.Id);
        
            var evaluations = _dbContext.Evaluations
                                    .Include(e=> e.EvaluatedUserProfile)
                                    .Include(e=> e.EvaluatorUserProfile)
                                    .Include(e=> e.CounterSignerUserProfile)
                                    .Where(e => e.EvaluatedUserProfileId == currentUserId ||  e.EvaluatorUserProfileId == currentUserId ||  e.CounterSignerUserProfileId == currentUserId);
        
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

                if (currentUserId == e.EvaluatedUserProfileId && e.Status == 5)
                { 
                    e.canFinished = true;
                }
            }
            return paginatedModel;
        }
    }
}