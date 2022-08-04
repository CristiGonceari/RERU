using CODWER.RERU.Personal.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Application.References.GetModernLanguages
{
    public class GetModernLanguageQuery : IRequest<List<SelectValue>>
    {
    }
}
