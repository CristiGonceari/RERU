using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.DataTransferObjects.Articles;
using CVU.ERP.Module.Application.TableExportServices;
using CVU.ERP.ServiceProvider;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Personal.Application.Articles.PrintArticles
{
    public class PrintArticlesCommandHandler : IRequestHandler<PrintArticlesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Article, ArticleDto> _printer;
        private readonly ICurrentApplicationUserProvider _currentApplication;

        public PrintArticlesCommandHandler(AppDbContext appDbContext, IExportData<Article, ArticleDto> printer, ICurrentApplicationUserProvider currentApplication)
        {
            _appDbContext = appDbContext;
            _printer = printer;
            _currentApplication = currentApplication;
        }

        public async Task<FileDataDto> Handle(PrintArticlesCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentApplication.Get();

            var articles = GetAndFilterArticles.Filter(_appDbContext, request.Name, currentUser);

            var result = _printer.ExportTableSpecificFormat(new TableData<Article>
            {
                Name = request.TableName,
                Items = articles,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
