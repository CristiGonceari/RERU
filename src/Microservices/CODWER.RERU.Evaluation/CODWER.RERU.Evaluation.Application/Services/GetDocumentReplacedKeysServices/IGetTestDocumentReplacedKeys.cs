using CVU.ERP.Common.DataTransferObjects.Files;
using RERU.Data.Entities;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services.GetDocumentReplacedKeysServices
{
    public interface IGetTestDocumentReplacedKeys
    {
        public Task<string> GetTestDocumentReplacedKey(Test test, int documentTemplateId);

        public Task<FileDataDto> GetPdf(string source, string testName);
    }
}
