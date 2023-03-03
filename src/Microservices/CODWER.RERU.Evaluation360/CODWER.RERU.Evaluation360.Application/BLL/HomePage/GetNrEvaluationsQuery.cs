using System.Collections.Generic;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.HomePage
{
    public class GetNrEvaluationsQuery : IRequest<List<int>>
    {
    }
}