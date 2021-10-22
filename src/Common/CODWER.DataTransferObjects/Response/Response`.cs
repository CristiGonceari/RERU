namespace CVU.ERP.Common.DataTransferObjects.Response {
    public class Response<T> : Response {
        public Response () : base () { }
        public Response (T data) : base () {
            Data = data;
        }

        public T Data { get; set; }
    }
}