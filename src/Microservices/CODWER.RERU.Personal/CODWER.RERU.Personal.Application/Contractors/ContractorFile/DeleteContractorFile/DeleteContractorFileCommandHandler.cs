using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.DeleteContractorFile
{
    public class DeleteContractorFileCommandHandler : IRequestHandler<DeleteContractorFileCommand, Unit>
    {
        private readonly IStorageFileService _fileService;
        private readonly AppDbContext _appDbContext;

        public DeleteContractorFileCommandHandler(IStorageFileService fileService, AppDbContext appDbContext)
        {
            _fileService = fileService;
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteContractorFileCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.ContractorFiles.FirstAsync(x => x.FileId == request.FileId);

            _appDbContext.ContractorFiles.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return await _fileService.RemoveFile(request.FileId);
        }
    }
}
