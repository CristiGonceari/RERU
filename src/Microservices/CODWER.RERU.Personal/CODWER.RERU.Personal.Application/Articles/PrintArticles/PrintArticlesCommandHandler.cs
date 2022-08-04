using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Articles;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Articles.PrintArticles
{
    public class PrintArticlesCommandHandler : IRequestHandler<PrintArticlesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Article, ArticleDto> _printer;

        public PrintArticlesCommandHandler(AppDbContext appDbContext, IExportData<Article, ArticleDto> printer)
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
                Orientation = request.Orientation
            });

            return result;
        }
    }
}
