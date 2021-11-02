using System;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IDismissalTemplateParserService
    {
        Task<int> SaveRequestFile(int contractorId, DateTime from);
        Task<int> SaveOrderFile(int contractorId, DateTime from);
    }
}
