using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IVacationTemplateParserService
    {
        public Task<string> SaveRequestFile(int contractorId, Vacation vacation);
        public Task<string> SaveOrderFile(int contractorId, Vacation vacation);
    }
}
