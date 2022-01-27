using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IVacationTemplateParserService
    {
        public Task<int> SaveRequestFile(int contractorId, Vacation vacation);
        public Task<int> SaveOrderFile(int contractorId, Vacation vacation);
    }
}
