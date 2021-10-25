using System.Collections.Generic;
namespace CVU.ERP.Common.DataTransferObjects.Response.Messages
{
    public class Message
    {
        public Message()
        {
            Data = new List<KeyValuePair<string, object>>();
        }
        public Message(MessageType type)
        {
            Type = type;
            Data = new List<KeyValuePair<string, object>>();
        }
        public string Code { set; get; }
        public string MessageText { set; get; }
        public MessageType Type { set; get; }
        public int Severity { set; get; }
        public string Field { set; get; }

        public IList<KeyValuePair<string, object>> Data { set; get; }
    }
}