using System.Threading.Tasks;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Common.Services.Identity
{
    public interface IIdentityService
    {
        string Type { get; }
        Task<string> Create(UserProfile userProfile, bool notify);
        Task Remove(string id);
        Task ResetPassword(string id);
    }
}