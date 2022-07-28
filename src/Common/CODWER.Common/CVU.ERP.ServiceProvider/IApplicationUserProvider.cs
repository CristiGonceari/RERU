using System.Threading.Tasks;
using CVU.ERP.ServiceProvider.Models;

namespace CVU.ERP.ServiceProvider
{
    /// <summary>
    ///  This interface will be used to provide an application user. 
    ///  The interface will have different implementations based on where it is used.
    ///  If the inteface is used in a Module, it should be provided with ModuleApplicationUserProvider.
    ///  Core module should have it's own implementation.
    /// </summary>
    public interface IApplicationUserProvider
    {
        /// <summary>
        ///  Gets an application user based on identity Id and the identityProvider
        /// </summary>
        Task<ApplicationUser> Get(string id, string identityProvider = null);
    }
}