﻿using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.References.GetMaterialStatusType
{
    public class GetMaterialStatusTypeQueryHandler : IRequestHandler<GetMaterialStatusTypeQuery, List<SelectValue>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetMaterialStatusTypeQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectValue>> Handle(GetMaterialStatusTypeQuery request, CancellationToken cancellationToken)
        {
            var materialStatusTypes = await _appDbContext.MaterialStatusTypes.ToListAsync();

            var mapper = _mapper.Map<List<SelectValue>>(materialStatusTypes);

            return mapper;
        }
    }
}
