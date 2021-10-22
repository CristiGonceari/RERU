using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response.Messages;
using CVU.ERP.Module.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace CVU.ERP.Module.Application.Infrastructure
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators.Select(x => new { Name = x.ToString(), Validator = x })
                .GroupBy(vObj => vObj.Name)
                .Select(vDist => vDist.First())
                .Select(v => v.Validator.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ApplicationRequestValidationException(failures.Select(f =>
                  new ValidationMessage
                  {
                      MessageText = f.ErrorMessage,
                      Code = f.ErrorCode
                  }).ToList());
            }

            return next();
        }
    }
}