using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using CVU.ERP.ServiceProvider;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Evaluation360;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.PrintEvaluations;

public class PrintEvaluationsCommandHandler : IRequestHandler<PrintEvaluationsCommand, FileDataDto>
{
    private readonly AppDbContext _appDbContext;
    private readonly IExportData<Evaluation, PrintTableEvaluationsDto> _printer;
    private readonly ICurrentApplicationUserProvider _currentUserProvider;

    public PrintEvaluationsCommandHandler(
        AppDbContext appDbContext, 
        IExportData<Evaluation, PrintTableEvaluationsDto> printer, 
        ICurrentApplicationUserProvider currentUserProvider)
    {
        _appDbContext = appDbContext;
        _printer = printer;
        _currentUserProvider = currentUserProvider;
    }
    
    public async Task<FileDataDto> Handle(PrintEvaluationsCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _currentUserProvider.Get();
        var currentUserId = int.Parse(currentUser.Id);
        
        var evaluations = _appDbContext.Evaluations
            .Include(e=> e.EvaluatedUserProfile)
            .Include(e=> e.EvaluatorUserProfile)
            .Include(e=> e.CounterSignerUserProfile)
            .Where(e => e.EvaluatedUserProfileId == currentUserId ||  
                        e.EvaluatorUserProfileId == currentUserId ||  
                        e.CounterSignerUserProfileId == currentUserId)
            .OrderByDescending(e => e.CreateDate)
            .AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(request.EvaluatedName))
        {
            var toSearch = request.EvaluatedName.Split(' ').ToList();

            foreach (var s in toSearch)
            {
                evaluations = evaluations.Where(p => p.EvaluatedUserProfile.FirstName.ToLower().Contains(s.ToLower())
                        || p.EvaluatedUserProfile.LastName.ToLower().Contains(s.ToLower())
                        || p.EvaluatedUserProfile.FatherName.ToLower().Contains(s.ToLower())); 
            }
        }

        if (!string.IsNullOrWhiteSpace(request.EvaluatorName))
        {
            var toSearch = request.EvaluatorName.Split(' ').ToList();

            foreach (var s in toSearch)
            {
                evaluations = evaluations.Where(p => p.EvaluatorUserProfile.FirstName.ToLower().Contains(s.ToLower())
                        || p.EvaluatorUserProfile.LastName.ToLower().Contains(s.ToLower())
                        || p.EvaluatorUserProfile.FatherName.ToLower().Contains(s.ToLower()));
            }
        }

        if (!string.IsNullOrWhiteSpace(request.CounterSignerName))
        {
            var toSearch = request.CounterSignerName.Split(' ').ToList();

            foreach (var s in toSearch)
            {
                evaluations = evaluations.Where(p => p.CounterSignerUserProfile.FirstName.ToLower().Contains(s.ToLower())
                        || p.CounterSignerUserProfile.LastName.ToLower().Contains(s.ToLower())
                        || p.CounterSignerUserProfile.FatherName.ToLower().Contains(s.ToLower()));
            }
        }

        if (request.Type.HasValue) 
        {
            evaluations = evaluations.Where(x => x.Type == request.Type); 
        }

        if (request.Points.HasValue)
        {
            evaluations = evaluations.Where(x => x.Points == request.Points);
        }

        if (request.Status.HasValue)
        {
            evaluations = evaluations.Where(x => x.Status == request.Status);
        }

        var result = _printer.ExportTableSpecificFormat(new TableData<Evaluation>
        {
            Name = "Evaluări360",
            Items = evaluations,
            Fields = request.Fields,
            Orientation = request.Orientation,
            ExportFormat = request.TableExportFormat
        });

        return result;
    }
}