namespace CVU.ERP.Common.DataTransferObjects.Response.Messages
{
    public class NotFoundErrorMessage : ErrorMessage
    {
        public NotFoundErrorMessage()
        {

        }
        public NotFoundErrorMessage(string errorMessage) : base(errorMessage)
        {
            Type = MessageType.Error;
            Severity = 3;
        }
    }
}