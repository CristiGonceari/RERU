using System;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;

namespace CVU.ERP.Module.Common.ExceptionHandlers {
    public interface IResponseExceptionHandler {
        Task<int> Handle (Exception exception, Response response);
    }

    public interface IResponseExceptionHandler<T> : IResponseExceptionHandler where T : Exception {
        Task<int> Handle (T exception, Response response);
    }
}