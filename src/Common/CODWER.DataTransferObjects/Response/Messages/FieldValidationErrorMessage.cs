namespace CVU.ERP.Common.DataTransferObjects.Response.Messages
{
    public class FieldValidationErrorMessage : ErrorMessage
    {
        public FieldValidationErrorMessage()
        {

        }
        public FieldValidationErrorMessage(string errorMessage, string field) : base(errorMessage)
        {
            MessageText = errorMessage;
            Type = MessageType.FieldValidationError;
            Field = field;
            Severity = 2;
        }
    }
}