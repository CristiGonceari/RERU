using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Departments.GetDepartmentsValue
{
    public class GetDepartmentsValueQueryHandler : IRequestHandler<GetDepartmentsValueQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetDepartmentsValueQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectItem>> Handle(GetDepartmentsValueQuery request, CancellationToken cancellationToken)
        {
            var departments = await _appDbContext.Departments
                .OrderBy(x => x.Name)
                .ToListAsync();

            return _mapper.Map<List<SelectItem>>(departments);
        }
    }
}
