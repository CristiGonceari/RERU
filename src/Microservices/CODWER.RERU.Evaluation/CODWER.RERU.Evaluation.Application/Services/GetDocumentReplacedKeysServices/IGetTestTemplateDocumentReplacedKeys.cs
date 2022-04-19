using CVU.ERP.Common.DataTransferObjects.Files;
using RERU.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services.GetDocumentReplacedKeysServices
{
    public interface IGetTestTemplateDocumentReplacedKeys
    {
        public Task<string> GetTestTemplateDocumentReplacedKey(TestTemplate testTemplate, int documentTemplateId);

        public Task<FileDataDto> GetPdf(string source, string testTemplateName);
    }
}
