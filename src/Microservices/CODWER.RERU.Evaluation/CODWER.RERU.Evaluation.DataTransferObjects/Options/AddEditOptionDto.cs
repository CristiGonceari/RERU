using CVU.ERP.StorageService.Models;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Options
{
    public class AddEditOptionDto
    {
        public int Id { get; set; }
        public int QuestionUnitId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
        public string? MediaFileId { get; set; }
        public AddFileDto FileDto { get; set; }
    }
}
