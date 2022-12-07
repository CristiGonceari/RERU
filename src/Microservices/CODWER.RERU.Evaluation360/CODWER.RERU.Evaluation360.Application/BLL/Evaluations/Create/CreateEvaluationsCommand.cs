using System;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Create
{
    public class CreateEvaluationsCommand : IRequest<Unit>
    {
        public int[] EvaluatedUserProfileIds { set; get; }
        public int CounterSignerUserProfileId { set; get; }
        public int Type { set; get; }
    }
}