using System;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IDismissalTemplateParserService
    {
        Task<string> SaveRequestFile(int contractorId, DateTime from);
        Task<string> SaveOrderFile(int contractorId, DateTime from);
    }
}
