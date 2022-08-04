using System.Threading.Tasks;
using CVU.ERP.ServiceProvider.Models;

namespace CVU.ERP.ServiceProvider
{
    ///<summary>
    /// Interface used to get the current authenticated application user
    ///</summary>
    public interface ICurrentApplicationUserProvider
    {
        bool IsAuthenticated { get; }
        string IdentityId { get; }
        string IdentityProvider { get; }
        Task<ApplicationUser> Get();

    }
}