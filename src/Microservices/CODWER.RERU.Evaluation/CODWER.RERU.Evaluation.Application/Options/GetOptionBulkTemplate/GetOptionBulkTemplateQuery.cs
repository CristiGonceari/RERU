using CODWER.RERU.Evaluation.Data.Entities.Enums;
using MediatR;


namespace CODWER.RERU.Evaluation.Application.Options.GetOptionBulkTemplate
{
    public class GetOptionBulkTemplateQuery : IRequest<byte[]>
    {
        public QuestionTypeEnum QuestionType { get; set; }
    }
}
