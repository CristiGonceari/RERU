using CVU.ERP.StorageService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.StorageService.Models;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.AddContractorFile
{
    public class AddContractorFileCommandHandler : IRequestHandler<AddContractorFileCommand, string>
    {
        private readonly IStorageFileService _fileService;
        private readonly AppDbContext _appDbContext;
        public AddContractorFileCommandHandler(IStorageFileService fileService, AppDbContext appDbContext)
        {
            _fileService = fileService;
            _appDbContext = appDbContext;
        }

        public async Task<string> Handle(AddContractorFileCommand request, CancellationToken cancellationToken)
        {
            var file = new AddFileDto
            {
                File = request.Data.File,
                Type = request.Data.Type
            };

            var addedFile = await _fileService.AddFile(file);

            var fileToAdd = new Data.Entities.Files.ContractorFile
            {
                ContractorId = request.Data.ContractorId,
                FileId = addedFile
            };

            await _appDbContext.ContractorFiles.AddAsync(fileToAdd);
            await _appDbContext.SaveChangesAsync();

            return addedFile;
        }
    }
}
