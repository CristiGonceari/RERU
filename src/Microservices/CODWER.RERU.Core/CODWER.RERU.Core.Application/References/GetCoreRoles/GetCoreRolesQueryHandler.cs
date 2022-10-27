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

namespace CODWER.RERU.Core.Application.References.GetCoreRoles
{
    public class GetCoreRolesQueryHandler : IRequestHandler<GetCoreRolesQuery, List<SelectItem>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetCoreRolesQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectItem>> Handle(GetCoreRolesQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.GetModuleRolePermissionsByPrefix(ModulePrefix.Core)
                .Select(x => x.Role)
                .Distinct()
                .Select(e => _mapper.Map<SelectItem>(e))
                .ToListAsync();
        }
    }
}