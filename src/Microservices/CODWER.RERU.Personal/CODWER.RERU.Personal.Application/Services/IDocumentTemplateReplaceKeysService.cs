using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IDocumentTemplateReplaceKeysService
    {
        public Task<Dictionary<string, string>> GetContractorGeneralValues(int contractorId); 
    }
}
