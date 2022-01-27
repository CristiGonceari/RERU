using CODWER.RERU.Personal.Data.Entities;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IContractorService
    {
        public Task<Contractor> GetCurrentContractor();
    }
}
