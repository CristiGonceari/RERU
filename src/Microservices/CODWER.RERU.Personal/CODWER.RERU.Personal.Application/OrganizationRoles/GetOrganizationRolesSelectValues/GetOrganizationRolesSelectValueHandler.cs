using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRolesSelectValues
{
    public class GetOrganizationRolesSelectValuesQueryHandler : IRequestHandler<GetOrganizationRolesSelectValuesQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetOrganizationRolesSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectItem>> Handle(GetOrganizationRolesSelectValuesQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.OrganizationRoles
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchWord))
            {
                items = items.Where(x => x.Name.Contains(request.SearchWord)
                                         || x.Code.Contains(request.SearchWord)
                                         || x.ShortCode.Contains(request.SearchWord));
            }

            return _mapper.Map<List<SelectItem>>(items);
        }
    }
}
