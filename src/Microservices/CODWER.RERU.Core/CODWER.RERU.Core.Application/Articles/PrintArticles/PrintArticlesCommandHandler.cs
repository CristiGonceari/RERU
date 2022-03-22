using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Module.Application.TablePrinterService;
using CODWER.RERU.Core.Data.Persistence.Context;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.DataTransferObjects.Articles;

namespace CODWER.RERU.Core.Application.Articles.PrintArticles
{
    public class PrintArticlesCommandHandler : IRequestHandler<PrintArticlesCommand, FileDataDto>
    {
        private readonly CoreDbContext _appDbContext;
        private readonly ITablePrinter<Article, ArticleDto> _printer;

        public PrintArticlesCommandHandler(CoreDbContext appDbContext, ITablePrinter<Article, ArticleDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintArticlesCommand request, CancellationToken cancellationToken)
        {
            var articles = GetAndFilterArticles.Filter(_appDbContext, request.Name);

            var result = _printer.PrintTable(new TableData<Article>
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
