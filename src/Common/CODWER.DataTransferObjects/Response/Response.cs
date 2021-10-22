using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.Response.Messages;

namespace CVU.ERP.Common.DataTransferObjects.Response {
    public class Response {
        public Response () {
            Success = true;
            Messages = new List<Message> ();
        }
        public bool Success { set; get; }

        public IList<Message> Messages { private set; get; }

        public void AddErrorMessage (string errorMessage) {
            AddErrorMessage (errorMessage, null);
        }

        public void AddErrorMessage (string errorMessage, string code) {
            AddErrorMessage (new ErrorMessage (errorMessage) { Code = code });
        }

        public void AddErrorMessage (Message message) {
            Messages.Add (message);
            Success = false;
        }

        public void AddErrorMessages (IEnumerable<Message> messages) {
            foreach (var mesage in messages) {
                Messages.Add (mesage);
            }

            Success = false;
        }

        public void AddMessage (Message message) {
            Messages.Add (message);
        }

        public void AddMessage (string message) {
            Messages.Add (new Message (MessageType.Info) { MessageText = message });
        }
    }
}