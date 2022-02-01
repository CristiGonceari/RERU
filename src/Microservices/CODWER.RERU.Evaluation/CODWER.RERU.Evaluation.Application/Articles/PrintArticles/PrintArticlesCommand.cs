using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Articles.PrintArticles
{
    public class PrintArticlesCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
    }
}
