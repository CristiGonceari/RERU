using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRolesSelectValues
{
    public class GetRolesSelectValuesQueryHandler : IRequestHandler<GetRolesSelectValuesQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetRolesSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectItem>> Handle(GetRolesSelectValuesQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Roles
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchWord))
            {
                items = items.Where(x => x.Name.Contains(request.SearchWord));
                //|| x.Code.Contains(request.SearchWord)
                //|| x.ShortCode.Contains(request.SearchWord));
            }

            return _mapper.Map<List<SelectItem>>(items);
        }
    }
}
