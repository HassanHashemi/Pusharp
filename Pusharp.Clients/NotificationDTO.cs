using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pusharp.Net.DTO
{


    public class NotificationDTO
    {
        public IconDTO Icon { get; set; }
        public string Ticker { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string AppId { get; set; }
        public CommandDTO Command { get; set; }
        public List<NotificationButtonDTO> Buttons { get; set; }

        public static NotificationDTO FromJson(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<NotificationDTO>(json);
            }
            catch(System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
