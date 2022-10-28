using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Evaluation360.Application.Interfaces
{
    public interface ISession
    {
        CurrentUser CurrentUser { get; set; }
        IHttpContextAccessor httpContextAccessor { get; set; }
    }
}
