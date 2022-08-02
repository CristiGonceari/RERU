using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations
{
    public class CandidatePositionService : ICandidatePositionService
    {
        private readonly AppDbContext _appDbContext;

        public CandidatePositionService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public string GetResponsiblePersonName(int id) => _appDbContext.UserProfiles
            .Select(u => new UserProfile
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                FatherName = u.FatherName,
                CreateById = u.CreateById
            })
            .FirstOrDefault(x => x.Id == id)?.FullName;

        public UserProfile GetResponsiblePerson(int id) => _appDbContext.UserProfiles
            .Select(u => new UserProfile
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                FatherName = u.FatherName,
                CreateById = u.CreateById,
                Email = u.Email
            })
            .FirstOrDefault(x => x.Id == id);
    }
}
