using System.Threading.Tasks;
using CVU.ERP.Module.Application.Models.Internal;

namespace CVU.ERP.Module.Application.Clients
{
    public interface IEvaluationClient
    {
        public Task SyncUserProfile(BaseUserProfile userProfile);
    }
}
