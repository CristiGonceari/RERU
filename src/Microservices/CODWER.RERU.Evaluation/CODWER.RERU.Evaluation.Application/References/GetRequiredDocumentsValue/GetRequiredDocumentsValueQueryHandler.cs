using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.References.GetRequiredDocumentsValue
{
    public class GetRequiredDocumentsValueQueryHandler : IRequestHandler<GetRequiredDocumentsValueQuery, List<SelectItem>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetRequiredDocumentsValueQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectItem>> Handle(GetRequiredDocumentsValueQuery request, CancellationToken cancellationToken)
        {
            var docs = await _appDbContext.RequiredDocuments
                .AsQueryable()
                .Select(e => _mapper.Map<SelectItem>(e))
                .ToListAsync();

            return docs;
        }
    }
}
