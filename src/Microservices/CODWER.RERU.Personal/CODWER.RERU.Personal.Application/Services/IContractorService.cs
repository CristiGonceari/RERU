using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IContractorService
    {
        public Task<Contractor> GetCurrentContractor();
    }
}
