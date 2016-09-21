using Newtonsoft.Json;

namespace MessageBus.Core
{
    public static class IMessageExtensions
    {
        public static string ToJson(this IMessageBusMessage source)
        {
            return JsonConvert.SerializeObject(source);
        }
    }
}