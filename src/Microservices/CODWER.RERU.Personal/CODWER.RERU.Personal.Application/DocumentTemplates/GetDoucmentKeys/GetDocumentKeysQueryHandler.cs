using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.GetDoucmentKeys
{
    
    public class GetDocumentKeysQueryHandler :IRequestHandler<GetDocumentKeysQuery, List<DocumentTemplateCategoryDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetDocumentKeysQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;

    }

        public async Task<List<DocumentTemplateCategoryDto>> Handle(GetDocumentKeysQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.HrDocumentTemplateCategories.Include(dc => dc.HrDocumentKeys).AsQueryable();

            var mappedItems = _mapper.Map<List<DocumentTemplateCategoryDto>>(items);

            return mappedItems;
        }
    }
    
}
