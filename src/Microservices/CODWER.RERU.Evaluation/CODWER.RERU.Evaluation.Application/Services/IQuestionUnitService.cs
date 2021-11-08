using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IQuestionUnitService
    {
        Task<QuestionUnit> GetHashedQuestionUnit(int questionUnitId);
        Task<QuestionUnit> GetUnHashedQuestionUnit(int questionUnitId);
        Task<string> GetUnHashedQuestionWithoutTags(int questionUnitId);
        Task HashQuestionUnit(int questionId);
        Task<byte[]> GenerateExcelTemplate(QuestionTypeEnum questionType);
        Task<byte[]> BulkQuestionsUpload(IFormFile input);
    }
}
