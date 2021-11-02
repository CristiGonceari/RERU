using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.Application.Interfaces
{
    public interface ISession
    {
        CurrentUser CurrentUser { get; set; }
        IHttpContextAccessor httpContextAccessor { get; set; }
    }
}
