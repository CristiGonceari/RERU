using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Attestations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Attestations.GetAttestations
{
    public class GetAttestationsQueryHandler : IRequestHandler<GetAttestationsQuery, PaginatedModel<AttestationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetAttestationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<AttestationDto>> Handle(GetAttestationsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Attestations
                .Include(x => x.Contractor)
                .AsQueryable();

            if (request.ContractorId != null)
            {
                items = items.Where(x => x.ContractorId == request.ContractorId);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Attestation, AttestationDto>(items, request);

            return paginatedModel;
        }
    }
}
