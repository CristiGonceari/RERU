namespace CVU.ERP.Common.DataTransferObjects.Response.Messages
{
    public class ErrorMessage : Message
    {
        public ErrorMessage()
        {

        }
        public ErrorMessage(string errorMessage, string code) : base(MessageType.Error)
        {
            MessageText = errorMessage;
            Code = code;
            Severity = 1;
        }

        public ErrorMessage(string errorMessage) : base(MessageType.Error)
        {
            MessageText = errorMessage;
            Severity = 1;
        }
    }
}