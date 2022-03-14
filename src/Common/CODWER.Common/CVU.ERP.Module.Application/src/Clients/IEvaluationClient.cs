using CVU.ERP.Module.Application.Models.Internal;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.TestDatas;

namespace CVU.ERP.Module.Application.Clients
{
    public interface IEvaluationClient
    {
        public Task SyncUserProfile(BaseUserProfile userProfile);
        public Task<TestDataDto> GetTestIdToStartTest();
    }
}
