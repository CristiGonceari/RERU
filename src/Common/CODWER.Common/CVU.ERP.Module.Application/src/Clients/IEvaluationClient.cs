using CVU.ERP.Module.Application.Models.Internal;
using System.Threading.Tasks;

namespace CVU.ERP.Module.Application.Clients
{
    public interface IEvaluationClient
    {
        public Task SyncUserProfile(BaseUserProfile userProfile);
        public Task<int> GetTestIdToStartTest();
    }
}
