using Newtonsoft.Json;
using System;

namespace Pusharp.Server.WebSockets
{
    public class ClientInfo
    {
        public string DeviceId { get; set; }
        public string AppId { get; set; }

        public static bool TryParseJson(string json, out ClientInfo info)
        {
            try
            {
                info = JsonConvert.DeserializeObject<ClientInfo>(json);
                return true;
            }
            catch (Exception)
            {
                info = null;
                return false;
            }
        }
    }
}