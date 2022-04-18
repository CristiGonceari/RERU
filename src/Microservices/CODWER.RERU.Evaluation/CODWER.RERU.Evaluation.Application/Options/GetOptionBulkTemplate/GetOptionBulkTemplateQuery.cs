using MediatR;
using RERU.Data.Entities.Enums;


namespace CODWER.RERU.Evaluation.Application.Options.GetOptionBulkTemplate
{
    public class GetOptionBulkTemplateQuery : IRequest<byte[]>
    {
        public QuestionTypeEnum QuestionType { get; set; }
    }
}
