using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contacts.AddContact
{
    public class AddContactCommandHandler : IRequestHandler<AddContactCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddContactCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddContactCommand request, CancellationToken cancellationToken)
        {
            var newContact = _mapper.Map<Contact>(request.Data);

            await _appDbContext.Contacts.AddAsync(newContact);
            await _appDbContext.SaveChangesAsync();

            return newContact.Id; ;
        }
    }
}
