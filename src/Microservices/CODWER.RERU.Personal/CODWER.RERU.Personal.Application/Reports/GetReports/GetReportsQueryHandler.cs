using System.Linq;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Personal.DataTransferObjects.Reports;
using CODWER.RERU.Personal.Application.Reports.GetReports;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
using CODWER.RERU.Personal.Data.Entities;

namespace CODWER.RERU.Personal.Application.Reports.GetReports
{
    public class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, PaginatedModel<ReportItemDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetReportsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<ReportItemDto>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.ByteFiles
                .Include(x => x.Contractor)
                    .ThenInclude(x => x.Positions)
                        .ThenInclude(x => x.Department)
                .Where(x => x.Type != FileTypeEnum.Identity)
                .AsQueryable();
     

            if (request.DepartmentId != null)
            {
                items = items.Where(x =>
                    x.Contractor.Positions.All(p => p.DepartmentId == request.DepartmentId));
            }

            if (request.Type != null)
            {
                items = items.Where(x => x.Type == request.Type);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                items = items.Where(x => x.FileName.Contains(request.Name));
            }

            if (!string.IsNullOrEmpty(request.ContractorName))
            {
                items = items.FilterOrdersByContractorName(request.ContractorName);
            }

            if (request.FromDate != null)
            {
                items = items.Where(x => x.CreateDate >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                items = items.Where(x => x.CreateDate <= request.ToDate);
            }

            items = SelectOnlyReturnedFields(items);

            return await _paginationService.MapAndPaginateModelAsync<ByteArrayFile, ReportItemDto>(items, request);
        }

        private IQueryable<ByteArrayFile> SelectOnlyReturnedFields(IQueryable<ByteArrayFile> items)
        {
            return items.Select(x => new ByteArrayFile
            {
                Id = x.Id,
                Type = x.Type,
                FileName = x.FileName,
                CreateDate = x.CreateDate,
                Contractor = new Contractor
                {
                    Id = x.Contractor.Id,
                    FirstName = x.Contractor.FirstName,
                    LastName = x.Contractor.LastName,
                    FatherName = x.Contractor.FatherName
                },
            });
        }
    }
}
