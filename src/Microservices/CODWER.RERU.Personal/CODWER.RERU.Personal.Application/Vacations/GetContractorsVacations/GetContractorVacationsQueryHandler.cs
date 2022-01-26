using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Vacations.GetContractorsVacations
{
    public class GetContractorVacationsQueryHandler : IRequestHandler<GetContractorVacationsQuery, PaginatedModel<VacationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IPaginationService _paginationService;

        public GetContractorVacationsQueryHandler(AppDbContext appDbContext, IMapper mapper, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<VacationDto>> Handle(GetContractorVacationsQuery request, CancellationToken cancellationToken)
        {
            var itemVacation = _appDbContext.Vacations
                .Include(x => x.Contractor)
                .Include(x => x.VacationRequest)
                .Include(x => x.VacationOrder)
                .Where(x => x.ContractorId == request.ContractorId)
                .OrderByDescending(x => x.Id);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Vacation, VacationDto>(itemVacation, request);

            return paginatedModel;

        }
    }
}
