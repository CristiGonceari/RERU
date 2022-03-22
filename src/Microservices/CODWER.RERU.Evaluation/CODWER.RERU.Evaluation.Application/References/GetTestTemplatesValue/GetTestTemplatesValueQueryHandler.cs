using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.References.GetTestTemplatesValue
{
    public class GetTestTemplatesValueQueryHandler : IRequestHandler<GetTestTemplatesValueQuery, List<SelectItem>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetTestTemplatesValueQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
        public async Task<List<SelectItem>> Handle(GetTestTemplatesValueQuery request, CancellationToken cancellationToken)
        {
            var testTemplates = await _appDbContext.TestTemplates
                .AsQueryable()
                .Where(tt => (tt.Status == (int)TestTemplateStatusEnum.Active || tt.Status == TestTemplateStatusEnum.Canceled) && tt.Mode == (int)TestTemplateModeEnum.Test)
                .Select(tt => _mapper.Map<SelectItem>(tt))
                .ToListAsync();

            return testTemplates;
        }

    }
}
