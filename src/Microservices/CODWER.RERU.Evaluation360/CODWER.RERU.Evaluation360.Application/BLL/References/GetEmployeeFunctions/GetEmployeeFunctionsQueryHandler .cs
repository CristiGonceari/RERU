using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace CODWER.RERU.Evaluation360.Application.References.GetEmployeeFunctions
{
    public class GetEmployeeFunctionsQueryHandler : IRequestHandler<GetEmployeeFunctionsQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public GetEmployeeFunctionsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<List<SelectItem>> Handle(GetEmployeeFunctionsQuery request, CancellationToken cancellationToken)
        {
            var employeeFunctions = _appDbContext.EmployeeFunctions
                .OrderBy(x => x.Name)
                .AsQueryable();

            return await employeeFunctions.Select(u => _mapper.Map<SelectItem>(u)).ToListAsync();
        }
    }
}