using CODWER.RERU.Personal.Data.Persistence.Context;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.Application.Validation
{
    public class ValidationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ISession _session;

        public ValidationService(AppDbContext appDbContext, ISession session)
        {
            _appDbContext = appDbContext;
            _session = session;
        }
    }
}
