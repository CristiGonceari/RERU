using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IVacationTemplateParserService
    {
        public Task<int> SaveRequestFile(int contractorId, Vacation vacation);
        public Task<int> SaveOrderFile(int contractorId, Vacation vacation);
    }
}
