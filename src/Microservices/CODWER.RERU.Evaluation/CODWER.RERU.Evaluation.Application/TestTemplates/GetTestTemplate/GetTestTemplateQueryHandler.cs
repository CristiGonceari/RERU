using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplate
{
    public class GetTestTemplateQueryHandler : IRequestHandler<GetTestTemplateQuery, TestTemplateDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestTemplateQueryHandler(AppDbContext appDbContext, 
            IMapper mapper
           )
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<TestTemplateDto> Handle(GetTestTemplateQuery request, CancellationToken cancellationToken)
        {
            var testTemplate = await _appDbContext.TestTemplates
                .Include(x => x.TestTemplateModuleRoles)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<TestTemplateDto>(testTemplate);

            mappedItem.Roles = await GetRoles(testTemplate.Id);

            return mappedItem;
        }

        private async Task<List<SelectItem>> GetRoles(int testTemplateId)
        {
            return await _appDbContext.TestTemplateModuleRoles
                .Include(x => x.ModuleRole)
                .Where(x => x.TestTemplateId == testTemplateId)
                .Select(x => new ModuleRole()
                {
                    Id = x.ModuleRole.Id,
                    Name = x.ModuleRole.Name
                })
                .Select(e => _mapper.Map<SelectItem>(e))
                .ToListAsync();
        }
    }
}
