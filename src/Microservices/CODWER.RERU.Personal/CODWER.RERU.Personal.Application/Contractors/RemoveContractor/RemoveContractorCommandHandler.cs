using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contractors.RemoveContractor
{
    public class RemoveContractorCommandHandler : IRequestHandler<RemoveContractorCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IStorageFileService _storageFileService;

        public RemoveContractorCommandHandler(AppDbContext appDbContext, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _storageFileService = storageFileService;
        }

        public async Task<Unit> Handle(RemoveContractorCommand request, CancellationToken cancellationToken)
        {
            var contractor = await _appDbContext.Contractors.FirstAsync(rt => rt.Id == request.Id);
            var userProfile = await _appDbContext.UserProfiles
                .Include(x => x.Contractor)
                .FirstOrDefaultAsync(up => up.Contractor.Id == request.Id);

            _appDbContext.Contractors.Remove(contractor);

            if (userProfile !=  null)
            {
                userProfile.Contractor = null;
            }

            RemoveAllData(request.Id);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private void RemoveAllData(int contractorId)
        { 
            var contractorFiles = _appDbContext.ContractorFiles.Where(x => x.ContractorId == contractorId);

            foreach (var item in contractorFiles)
            {
                _storageFileService.RemoveFile(item.FileId);
            }

            _appDbContext.ContractorFiles.RemoveRange(contractorFiles);

            var contractorBulletins = _appDbContext.Bulletins
                .Include(x=>x.UserProfile)
                .ThenInclude(x=>x.Contractor)
                .Where(x => x.UserProfile.Contractor.Id == contractorId);
            _appDbContext.Bulletins.RemoveRange(contractorBulletins);

            var contractorStudies = _appDbContext.Studies
                .Include(x => x.UserProfile)
                .ThenInclude(x => x.Contractor)
                .Where(x => x.UserProfile.Contractor.Id == contractorId);

            _appDbContext.Studies.RemoveRange(contractorStudies);
        }
    }
}
