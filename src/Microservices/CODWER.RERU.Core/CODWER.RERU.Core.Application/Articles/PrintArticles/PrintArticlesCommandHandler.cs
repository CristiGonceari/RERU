﻿using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using CVU.ERP.Module.Application.TableExportServices;
using CVU.ERP.Module.Application.TableExportServices.Interfaces;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Articles.PrintArticles
{
    public class PrintArticlesCommandHandler : IRequestHandler<PrintArticlesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<ArticleCore, ArticleCoreDto> _printer;

        public PrintArticlesCommandHandler(AppDbContext appDbContext, IExportData<ArticleCore, ArticleCoreDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintArticlesCommand request, CancellationToken cancellationToken)
        {
            var articles = GetAndFilterArticles.Filter(_appDbContext, request.Name);

            var result = _printer.ExportTableSpecificFormat(new TableData<ArticleCore>
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
