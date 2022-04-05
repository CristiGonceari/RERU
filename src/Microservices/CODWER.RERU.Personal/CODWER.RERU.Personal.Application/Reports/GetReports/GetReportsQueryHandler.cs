using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Reports;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Reports.GetReports
{
    public class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, PaginatedModel<ReportItemDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IPersonalStorageClient _personalStorageClient;
        public GetReportsQueryHandler(AppDbContext appDbContext, 
            IPaginationService paginationService, 
            IPersonalStorageClient personalStorageClient)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _personalStorageClient = personalStorageClient;
        }


        public async Task<PaginatedModel<ReportItemDto>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
        {
            var contractorFiles = _appDbContext.ContractorFiles
                .Include(x => x.Contractor)
                    .ThenInclude(x => x.Positions)
                        .ThenInclude(x => x.Department)
                .OrderBy(x => x.ContractorId)
                .AsQueryable();


            contractorFiles = FilterContractors(contractorFiles, request);

            var files = await _personalStorageClient.GetContractorFiles(contractorFiles.Select(x => x.FileId).ToList());

            files = FilterFiles(files, request);

            var paginatedList = await _paginationService.MapAndPaginateModelAsync<File, ReportItemDto>(files, request);

            return await GetFileReport(paginatedList, contractorFiles.ToList());
        }

        private async Task<PaginatedModel<ReportItemDto>> GetFileReport(PaginatedModel<ReportItemDto> paginatedList, List<ContractorFile> contractorFiles)
        {
            foreach (var item in paginatedList.Items)
            {
               var contractor = contractorFiles.FirstOrDefault(x => x.FileId == item.Id);

               item.ContractorId = contractor.ContractorId;
               item.ContractorName = contractor.Contractor.FirstName;
               item.ContractorLastName = contractor.Contractor.LastName;
               item.ContractorFatherName = contractor.Contractor.FatherName;
            }

            return paginatedList;
        }

        private IQueryable<ContractorFile> FilterContractors(IQueryable<ContractorFile> contractorFiles, GetReportsQuery request)
        {

            if (request.DepartmentId != null)
            {
                contractorFiles = contractorFiles.Where(x =>
                    x.Contractor.Positions.All(p => p.DepartmentId == request.DepartmentId));
            }

            if (!string.IsNullOrEmpty(request.ContractorName))
            {
                contractorFiles = contractorFiles.FilterOrdersByContractorName(request.ContractorName);
            }

            return contractorFiles;
        }

        private IQueryable<File> FilterFiles(IQueryable<File> files, GetReportsQuery request)
        {
            if (request.FileType != null)
            {
                files = files.Where(x => x.FileType == request.FileType);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                files = files.Where(x => x.FileName.ToLower().Contains(request.Name.ToLower()));
            }

            if (request.FromDate != null)
            {
                files = files.Where(x => x.CreateDate >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                files = files.Where(x => x.CreateDate <= request.ToDate);
            }

            return files;
        }
    }
}
