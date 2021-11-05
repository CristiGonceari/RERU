using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Evaluation.Application.Interfaces
{
    public interface ISession
    {
        CurrentUser CurrentUser { get; set; }
        IHttpContextAccessor httpContextAccessor { get; set; }
    }
}
