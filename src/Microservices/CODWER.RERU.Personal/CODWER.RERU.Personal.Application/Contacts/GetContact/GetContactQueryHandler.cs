using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contacts.GetContact
{
    public class GetContactQueryHandler : IRequestHandler<GetContactQuery, ContactDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetContactQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ContactDto> Handle(GetContactQuery request, CancellationToken cancellationToken)
        {
            var contact = _appDbContext.Contacts
                .Include(r => r.Contractor)
                .First(rt => rt.Id == request.Id);

            var mappedData = _mapper.Map<ContactDto>(contact);

            return mappedData;
        }
    }
}
