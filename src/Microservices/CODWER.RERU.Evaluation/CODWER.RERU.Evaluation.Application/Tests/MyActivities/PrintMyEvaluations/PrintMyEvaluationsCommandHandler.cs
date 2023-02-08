using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.PrintMyEvaluations
{
    internal class PrintMyEvaluationsCommandHandler : IRequestHandler<PrintMyEvaluationsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IExportData<Test, TestDto> _printer;

        public PrintMyEvaluationsCommandHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IExportData<Test, TestDto> printer)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintMyEvaluationsCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var myEvaluations = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .Where(t => t.EvaluatorId == currentUserId && t.TestTemplate.Mode == TestTemplateModeEnum.Evaluation)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (request.FromDate.HasValue)
            {
                myEvaluations = myEvaluations.Where(x => x.EndTime >= request.FromDate);
            }

            if (request.ToDate.HasValue)
            {
                myEvaluations = myEvaluations.Where(x => x.EndTime <= request.ToDate);
            }

            if (!string.IsNullOrWhiteSpace(request.EvaluatedName))
            {
                myEvaluations = myEvaluations.Where(x => x.UserProfile.FirstName.ToLower().Contains(request.EvaluatedName.ToLower())
                                                         || x.UserProfile.LastName.ToLower().Contains(request.EvaluatedName.ToLower())
                                                         || x.UserProfile.FatherName.ToLower().Contains(request.EvaluatedName.ToLower())
                                                         || x.UserProfile.Idnp.ToLower().Contains(request.EvaluatedName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(request.EvaluationName))
            {
                myEvaluations = myEvaluations.Where(x => x.TestTemplate.Name.ToLower().Contains(request.EvaluationName.ToLower()));
            }

            var result = _printer.ExportTableSpecificFormat(new TableData<Test>
            {
                Name = request.TableName,
                Items = myEvaluations,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
