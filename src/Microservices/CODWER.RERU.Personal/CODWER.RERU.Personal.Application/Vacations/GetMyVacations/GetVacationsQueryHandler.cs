using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Vacations.GetMyVacations
{
    public class GetMyVacationsHandler : IRequestHandler<GetMyVacationsQuery, PaginatedModel<VacationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetMyVacationsHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<VacationDto>> Handle(GetMyVacationsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Vacations
                .Include(x => x.Contractor)
                .Include(x => x.VacationType)
                .AsQueryable();

            var userProfile = await _userProfileService.GetCurrentUserProfile();

            Console.WriteLine("----GetMyVacation : ContractorId = " + userProfile.ContractorId);
            
            if (userProfile?.ContractorId != null)
            {
                items = items.Where(x => x.ContractorId == userProfile.ContractorId);
            }
            else
            {
                items = items.Where(x => false);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Vacation, VacationDto>(items, request);

            return paginatedModel;
        }
    }
}
