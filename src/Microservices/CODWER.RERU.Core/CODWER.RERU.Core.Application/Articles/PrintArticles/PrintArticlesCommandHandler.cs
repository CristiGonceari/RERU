using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Data.Persistence.Context;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using CVU.ERP.Module.Application.TableExportServices;
using CVU.ERP.Module.Application.TableExportServices.Interfaces;

namespace CODWER.RERU.Core.Application.Articles.PrintArticles
{
    public class PrintArticlesCommandHandler : IRequestHandler<PrintArticlesCommand, FileDataDto>
    {
        private readonly CoreDbContext _appDbContext;
        private readonly IExportData<Article, ArticleDto> _printer;

        public PrintArticlesCommandHandler(CoreDbContext appDbContext, IExportData<Article, ArticleDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintArticlesCommand request, CancellationToken cancellationToken)
        {
            var articles = GetAndFilterArticles.Filter(_appDbContext, request.Name);

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
