﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.ModulePrefixes;

namespace CODWER.RERU.Personal.Application.References.GetPersonalRoles
{
    public class GetPersonalRolesQueryHandler : IRequestHandler<GetPersonalRolesQuery, List<SelectItem>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetPersonalRolesQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectItem>> Handle(GetPersonalRolesQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.GetModuleRolePermissions(ModulePrefix.Personal)
                .Select(x => x.Role)
                .Distinct()
                .Select(e => _mapper.Map<SelectItem>(e))
                .ToListAsync();
        }
    }
}
