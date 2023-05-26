using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.References.GetDepartmentsValue
{
    public class GetDepartmentsValuesQueryHandler : IRequestHandler<GetDepartmentsValuesQuery, List<SelectItem>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetDepartmentsValuesQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectItem>> Handle(GetDepartmentsValuesQuery request, CancellationToken cancellationToken)
        {
            var departments = _appDbContext.Departments
                .OrderBy(x=>x.Name)
                .AsQueryable();

            return _mapper.Map<List<SelectItem>>(departments);
        }
    }
}
