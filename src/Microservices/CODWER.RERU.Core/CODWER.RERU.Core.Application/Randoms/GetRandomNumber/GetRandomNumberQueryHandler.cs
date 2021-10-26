using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Randoms.GetRandomNumber
{
    public class GetRandomNumberQueryHandler : IRequestHandler<GetRandomNumberQuery, string>
    {
        public Task<string> Handle(GetRandomNumberQuery request, CancellationToken cancellationToken)
        {
            var random = new Random();
            string chars = "0123456789";

            return Task.FromResult(
                new string(Enumerable.Repeat(chars, 4)
                    .Select(s => s[random.Next(s.Length)]).ToArray())
            );
        }
    }
}
