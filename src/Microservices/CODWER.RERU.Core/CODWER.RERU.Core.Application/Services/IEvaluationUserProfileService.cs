using System.Threading.Tasks;
using CVU.ERP.Module.Application.Models.Internal;

namespace CODWER.RERU.Core.Application.Services
{
    public interface IEvaluationUserProfileService
    {
        public Task Sync(BaseUserProfile userProfile);
    }
}
