using RERU.Data.Entities.PersonalEntities;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IContractorService
    {
        public Task<Contractor> GetCurrentContractor();
    }
}
