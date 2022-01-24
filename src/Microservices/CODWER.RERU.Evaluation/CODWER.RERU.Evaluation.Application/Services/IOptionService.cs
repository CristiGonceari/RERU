using CODWER.RERU.Evaluation.Data.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IOptionService
    {
        Task<byte[]> GenerateExcelTemplate(QuestionTypeEnum questionType);
        Task<byte[]> BulkOptionsUpload(IFormFile input, int questionUnitId);
    }
}
