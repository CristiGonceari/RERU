using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.Departments.GetDepartmentsSelectValues
{

    public class GetDepartmentsSelectValuesQueryHandler : IRequestHandler<GetDepartmentsSelectValuesQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetDepartmentsSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectItem>> Handle(GetDepartmentsSelectValuesQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Departments
                .OrderBy(x=>x.Name)
                .AsQueryable();

            var mappedItems = _mapper.Map<List<SelectItem>>(items);

            return mappedItems;
        }
    }
}
