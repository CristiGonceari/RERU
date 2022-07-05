using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;

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
            var docs = Filter(request.Name) ;

            if (!string.IsNullOrEmpty(request.Name))
            {
                docs = docs.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var selectItems = await docs.Select(e => _mapper.Map<SelectItem>(e))
                .ToListAsync();

            return selectItems;
        }

        public IQueryable<RequiredDocument> Filter(string name)
        {
            var items = _appDbContext.RequiredDocuments.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                items = items.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            return items;
        }
    }
}
