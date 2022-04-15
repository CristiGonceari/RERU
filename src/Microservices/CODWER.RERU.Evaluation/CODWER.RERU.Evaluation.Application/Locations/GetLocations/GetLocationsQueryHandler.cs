﻿using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Locations.GetLocations
{
    public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, PaginatedModel<LocationDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;

        public GetLocationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
        }

        public async Task<PaginatedModel<LocationDto>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = GetAndFilterLocations.Filter(_appDbContext, request.Name, request.Address);

            return await _paginationService.MapAndPaginateModelAsync<Location, LocationDto>(locations, request);
        }
    }
}
