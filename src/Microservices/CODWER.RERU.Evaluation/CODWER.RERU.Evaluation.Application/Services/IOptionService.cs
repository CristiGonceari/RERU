using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IOptionService
    {
        Task<byte[]> GenerateExcelTemplate(QuestionTypeEnum questionType);
        Task<byte[]> BulkOptionsUpload(IFormFile input, int questionUnitId);
    }
}
