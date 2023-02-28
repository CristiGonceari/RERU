using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.PrintMySolicitedPositions
{
    public class PrintMySolicitedPositionsCommandHandler : IRequestHandler<PrintMySolicitedPositionsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<SolicitedVacantPosition, SolicitedCandidatePositionDto> _printer;
        private readonly IUserProfileService _userProfileService;

        public PrintMySolicitedPositionsCommandHandler(IExportData<SolicitedVacantPosition, SolicitedCandidatePositionDto> printer, 
            IUserProfileService userProfileService, 
            AppDbContext appDbContext)
        {
            _printer = printer;
            _userProfileService = userProfileService;
            _appDbContext = appDbContext;
        }

        public async Task<FileDataDto> Handle(PrintMySolicitedPositionsCommand request, CancellationToken cancellationToken)
        {
            var currentUserProfileId = await _userProfileService.GetCurrentUserId();

            var solicitedTest = _appDbContext.SolicitedVacantPositions
                .Include(t => t.UserProfile)
                .Include(t => t.CandidatePosition)
                .Include(x => x.SolicitedVacantPositionUserFiles)
                .Include(x => x.CandidatePosition.RequiredDocumentPositions)
                .Where(t => t.UserProfileId == currentUserProfileId)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            var result = _printer.ExportTableSpecificFormat(new TableData<SolicitedVacantPosition>
            {
                Name = request.TableName,
                Items = solicitedTest,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
