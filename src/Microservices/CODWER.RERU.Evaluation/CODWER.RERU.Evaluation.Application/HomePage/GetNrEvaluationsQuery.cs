using System.Collections.Generic;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.HomePage
{
    public class GetNrEvaluationsQuery : IRequest<List<int>>
    {
    }
}