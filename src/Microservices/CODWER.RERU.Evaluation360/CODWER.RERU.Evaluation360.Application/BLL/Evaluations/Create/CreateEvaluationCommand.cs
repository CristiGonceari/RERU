using System;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Create
{
    public class CreateEvaluationCommand : IRequest<int>
    {
        public int EvaluatedUserProfileId { set; get; }
        public int CounterSignerUserProfileId { set; get; }
        public int Type { set; get; }
    }
}