
namespace MessageBus.Core
{
    public class JSONMessage : IMessageBusMessage
    {
        private string _rawJson;
        public JSONMessage(string rawJson)
        {
            this._rawJson = rawJson;
        }

        public string Data
        {
            get
            {
                return _rawJson;
            }
            set
            {
                _rawJson = value;
            }
        }
    }

}