using CODWER.RERU.Evaluation360.Application.Interfaces;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.Validation
{
    public class ValidationService
    {
        private readonly AppDbContext appDbContext;
        private readonly ISession session;

        public ValidationService(AppDbContext appDbContext, ISession session)
        {
            this.appDbContext = appDbContext;
            this.session = session;
        }
    }
}
