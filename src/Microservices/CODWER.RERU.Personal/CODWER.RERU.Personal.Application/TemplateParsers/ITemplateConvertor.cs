using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.Application.TemplateParsers
{
    public interface ITemplateConvertor
    {
        public Task<FileDataDto> GetPdfFromFile(IFormFile file);
        public Task<FileDataDto> GetPdfFromHtmlString(HtmlContentDto content);
        public Task<FileDataDto> GetPdfFromHtml(Dictionary<string, string> dictionary, string fileName);
    }
}
