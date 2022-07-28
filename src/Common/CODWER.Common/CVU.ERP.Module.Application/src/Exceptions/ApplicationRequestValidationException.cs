//using System;
//using System.Collections.Generic;
//using CVU.ERP.Common.DataTransferObjects.Response.Messages;

//namespace CVU.ERP.Module.Application.Exceptions
//{
//    public class ApplicationRequestValidationException : Exception
//    {
//        public ApplicationRequestValidationException(List<ValidationMessage> validationMessages)
//        {
//            ValidationMessages = validationMessages;
//        }

//        public List<ValidationMessage> ValidationMessages { private set; get; }
//    }

//    public class ValidationMessage
//    {
//        public string MessageText { get; set; }
//        public string Code { get; set; }
//    }
//}