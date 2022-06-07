using AutoMapper;
using CODWER.RERU.Core.Application.Common.Services;
using CODWER.RERU.Core.DataTransferObjects.Files;
using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.ExportUserTestsList
{
    public class ExportUserTestsListCommandHandler : IRequestHandler<ExportUserTestsListCommand, ExportExcel>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportUserTestsService _exportUserTestsService;
        private readonly IMapper _mapper;

        public ExportUserTestsListCommandHandler(AppDbContext appDbContext, IExportUserTestsService exportUserTestsService, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _exportUserTestsService = exportUserTestsService;
            _mapper = mapper;
        }
        public async Task<ExportExcel> Handle(ExportUserTestsListCommand request, CancellationToken cancellationToken)
        {
            var userTests = _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .OrderByDescending(x => x.CreateDate)
                .Where(x => x.UserProfileId == request.UserId)
                .Select(t => new Test
                {
                    Id = t.Id,
                    UserProfile = t.UserProfile,
                    TestTemplate = t.TestTemplate,
                    TestQuestions = t.TestQuestions,
                    Event = t.Event,
                    AccumulatedPercentage = t.AccumulatedPercentage,
                    EventId = t.EventId,
                    ResultStatus = t.ResultStatus,
                    TestStatus = t.TestStatus,
                    TestTemplateId = t.TestTemplateId
                }).AsQueryable();

            var mappedData = _mapper.Map<List<UserTestDto>>(userTests);

            var exportFile = await _exportUserTestsService.DonwloadUserTestsExcel(mappedData);

            return exportFile;

        }
    }
}
