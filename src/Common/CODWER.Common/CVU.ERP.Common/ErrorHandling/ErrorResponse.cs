namespace CVU.ERP.Common.ErrorHandling
{ 
    public sealed class ErrorResponse
    {
        public string Message { get; set; }

        public string Detail { get; set; }

        public object Validation { get; set; }
    }
}
