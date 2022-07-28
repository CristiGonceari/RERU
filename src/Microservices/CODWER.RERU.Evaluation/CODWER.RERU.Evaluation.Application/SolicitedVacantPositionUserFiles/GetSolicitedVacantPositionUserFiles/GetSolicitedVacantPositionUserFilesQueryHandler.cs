using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using CVU.ERP.ServiceProvider;
using CVU.ERP.ServiceProvider.Models;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.GetSolicitedVacantPositionUserFiles
{
    public class GetSolicitedVacantPositionUserFilesQueryHandler : IRequestHandler<GetSolicitedVacantPositionUserFilesQuery, List<GetSolicitedVacantPositionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IStorageFileService _storageFileService;
        private readonly ICurrentApplicationUserProvider _currentApplication;
        private ApplicationUser _user;

        public GetSolicitedVacantPositionUserFilesQueryHandler(AppDbContext appDbContext, 
            IStorageFileService storageFileService, ICurrentApplicationUserProvider currentApplication)
        {
            _appDbContext = appDbContext;
            _storageFileService = storageFileService;
            _currentApplication = currentApplication;
        }

        public async Task<List<GetSolicitedVacantPositionDto>> Handle(GetSolicitedVacantPositionUserFilesQuery request, CancellationToken cancellationToken)
        {
            var userFilesList = await GetRequiredDocumentsByCandidatePosition(request);

            return userFilesList;
        }

        private async Task<List<GetSolicitedVacantPositionDto>> GetRequiredDocumentsByCandidatePosition(GetSolicitedVacantPositionUserFilesQuery request)
        {
            var solicitedFilesList = new List<GetSolicitedVacantPositionDto>();

            if (request.UserId == null)
            {
                _user = await _currentApplication.Get();
            }

            var userFiles = await _appDbContext.SolicitedVacantPositionUserFiles
                .Where(x => x.SolicitedVacantPositionId == request.SolicitedVacantPositionId)
                .Include(c => c.RequiredDocument)
                .Include(c => c.UserProfile)
                .AsQueryable()
                .Where(x => x.UserProfileId == (request.UserId ?? int.Parse(_user.Id)) &&
                            x.SolicitedVacantPositionId == request.SolicitedVacantPositionId)
                .ToListAsync();

            var requiredFiles = _appDbContext.RequiredDocumentPositions
                .Include(x => x.RequiredDocument)
                .Where(x => x.CandidatePositionId == request.CandidatePositionId);


            foreach (var reqFile in requiredFiles)
            {
                var file = userFiles.FirstOrDefault(x => x.RequiredDocumentId == reqFile.RequiredDocumentId);

                if (file == null) 
                {
                    var item = new GetSolicitedVacantPositionDto
                    {
                        FileId = string.Empty,
                        FileName = string.Empty,
                        RequiredDocumentId = reqFile.RequiredDocumentId,
                        RequiredDocumentName = reqFile.RequiredDocument.Name
                    };

                    solicitedFilesList.Add(item);
                }
                else
                {
                    var fileName = await _storageFileService.GetFileName(file.FileId);

                    var item = new GetSolicitedVacantPositionDto
                    {
                        FileId = file.FileId,
                        FileName = fileName,
                        RequiredDocumentId = reqFile.RequiredDocumentId,
                        RequiredDocumentName = file.RequiredDocument.Name
                    };

                    solicitedFilesList.Add(item);
                }
            }

            return solicitedFilesList;
        }
    }
}
