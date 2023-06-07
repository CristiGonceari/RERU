using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.StorageService.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateByStatus
{
    public class GetTestTemplateByStatusQueryHandler : IRequestHandler<GetTestTemplateByStatusQuery, List<SelectTestTemplateValueDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly StorageDbContext _storageDbContext;
        private readonly IMapper _mapper;

        public GetTestTemplateByStatusQueryHandler(AppDbContext appDbContext,
            IMapper mapper,
            StorageDbContext storageDbContext)
        {
            _appDbContext = appDbContext.NewInstance();
            _mapper = mapper;
            _storageDbContext = storageDbContext.NewInstance();
        }

        public async Task<List<SelectTestTemplateValueDto>> Handle(GetTestTemplateByStatusQuery request, CancellationToken cancellationToken)
        {
            var testTemplates = _appDbContext.TestTemplates
                    .Include(x => x.EventTestTemplates)
                    .Where(x => x.Status == request.TestTemplateStatus)
                    .Select(t => new TestTemplate
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Mode = t.Mode,
                        IsGridTest = t.IsGridTest == null ? null : t.IsGridTest,
                        EventTestTemplates = t.EventTestTemplates.Select(et => new EventTestTemplate
                        {
                            EventId = et.EventId
                        }).ToList()
                    })
                    .AsQueryable();

            if (request.Mode != null)
            {
                testTemplates = testTemplates.Where(x => x.Mode == request.Mode);
            }

            if (request.EventId.HasValue)
            {
                testTemplates = testTemplates.Where(x => x.EventTestTemplates.Any(e => e.EventId == request.EventId));
            }

            var selectTestTemplateValueDto = testTemplates.Select(x => _mapper.Map<SelectTestTemplateValueDto>(x)).ToList();

            foreach (var testTemplateDto in selectTestTemplateValueDto)
            {
                var questionsList = new List<QuestionUnitDto>();

                _mapper.Map<SelectTestTemplateValueDto>(testTemplateDto);

                testTemplateDto.PrintTest = await CanPrintTest(questionsList);
            }

            return selectTestTemplateValueDto;
        }

        private async Task<bool> CanPrintTest(List<QuestionUnitDto> questionsList)
        {
            var mediaList = new List<string>() { "video", "audio", "gif" };
            var filesIdsList = new List<string>();

            var mediaOptionsIds = await _appDbContext.Options
                .Where(o => questionsList
                    .Select(x => x.Id)
                    .Contains(o.QuestionUnitId))
                .Where(o => o.MediaFileId != null)
                .Select(o => o.MediaFileId)
                .ToListAsync();

            var mediaQuestionsIds = questionsList
                .Where(q => q.MediaFileId != null)
                .Select(o => o.MediaFileId)
                .ToList();

            filesIdsList.AddRange(mediaOptionsIds);
            filesIdsList.AddRange(mediaQuestionsIds);

            var files = _storageDbContext.Files
                .Where(f => filesIdsList.Contains(f.Id.ToString()))
                .ToList();

            var result = !files.Any(x => mediaList.Any(m => x.Type.Contains(m)));

            return result;
        }
    }
}
