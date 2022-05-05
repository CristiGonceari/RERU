using MediatR;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.RegistrationPageMessage.AddEditRegistrationPageMessage
{
    public class AddEditRegistrationPageMessageCommandHandler : IRequestHandler<AddEditRegistrationPageMessageCommand, int>
    {
        private readonly AppDbContext _appDbContext;

        public AddEditRegistrationPageMessageCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Handle(AddEditRegistrationPageMessageCommand request, CancellationToken cancellationToken)
        {
            var message = _appDbContext.RegistrationPageMessages.First();

            message.Message = request.Message;

            await _appDbContext.SaveChangesAsync();

            return message.Id;
        }
    }
}
