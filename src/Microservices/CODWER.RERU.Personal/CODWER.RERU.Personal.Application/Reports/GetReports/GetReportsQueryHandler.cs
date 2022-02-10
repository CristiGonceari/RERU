using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Reports;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Reports.GetReports
{
    public class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, PaginatedModel<ReportItemDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly StorageDbContext _storageDbContext;
        public GetReportsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, StorageDbContext storageDbContext)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _storageDbContext = storageDbContext;
        }

        public async Task<PaginatedModel<ReportItemDto>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.ContractorFiles
                .Include(x => x.Contractor)
                    .ThenInclude(x => x.Positions)
                        .ThenInclude(x => x.Department)
                .AsQueryable();

            var files = _storageDbContext.Files
                .Where(x => x.FileType == FileTypeEnum.identityfiles && 
                            items.Any(i => i.FileId == x.Id.ToString()))
                .Select(f => new File
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    Type = f.Type,
                    FileType = f.FileType
                })
                .AsQueryable();

            if (request.DepartmentId != null)
            {
                items = items.Where(x =>
                    x.Contractor.Positions.All(p => p.DepartmentId == request.DepartmentId));
            }

            if (request.FileType != null)
            {
                files = files.Where(x => x.FileType == request.FileType);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                files = files.Where(x => x.FileName.Contains(request.Name));
            }

            if (!string.IsNullOrEmpty(request.ContractorName))
            {
                items = items.FilterOrdersByContractorName(request.ContractorName);
            }

            if (request.FromDate != null)
            {
                files = files.Where(x => x.CreateDate >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                files = files.Where(x => x.CreateDate <= request.ToDate);
            }

            var paginatedList = await _paginationService.MapAndPaginateModelAsync<ContractorFile, ReportItemDto>(items, request);

            return await GetFileReport(paginatedList, files);
        }

        private async Task<PaginatedModel<ReportItemDto>> GetFileReport(PaginatedModel<ReportItemDto> paginatedList, IQueryable<File> files)
        {
            foreach (var item in paginatedList.Items)
            {
                var file = files.FirstOrDefault(x => x.Id.ToString() == item.Id);

                if (file == null) continue;
                item.Type = file.FileType;
                item.FileName = file.FileName;
                item.CreateDate = file.CreateDate;
            }

            return paginatedList;
        }
    }
}
