using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contacts.GetContacts
{
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, PaginatedModel<ContactDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetContactsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = _appDbContext.Contacts
                .Include(r => r.Contractor)
                .AsQueryable();

            if (request.ContractorId != null)
            {
                contacts = contacts.Where(x => x.ContractorId == request.ContractorId.Value);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Contact, ContactDto>(contacts, request);

            return paginatedModel;
        }
    }
}
