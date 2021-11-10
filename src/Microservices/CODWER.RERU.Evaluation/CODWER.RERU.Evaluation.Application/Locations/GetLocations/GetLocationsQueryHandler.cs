﻿using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var locations = _appDbContext.Locations.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                locations = locations.Where(x => EF.Functions.Like(x.Name, $"%{request.Name}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.Address))
            {
                locations = locations.Where(x => EF.Functions.Like(x.Address, $"%{request.Address}%"));
            }

            return _paginationService.MapAndPaginateModel<Location, LocationDto>(locations, request);
        }
    }
}
