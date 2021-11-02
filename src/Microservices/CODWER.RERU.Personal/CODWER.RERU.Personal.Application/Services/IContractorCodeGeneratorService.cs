using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IContractorCodeGeneratorService
    {
        Task<string> Next();
    }
}
