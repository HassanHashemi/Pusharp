using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Pusharp.Server.WebSockets.AppEvents
{
    public static class AppEventManager
    {
        public static void ExecuteHandler(AppEventType eventType)
        {
            foreach(var handler in GetHandler(eventType))
            {
                handler.Execute_AppHandler();
            }
        }

        private static IEnumerable<IAppEvent> GetHandler(AppEventType eventType)
        {
            Type interfaceType = typeof(IAppEvent);
            List<IAppEvent> result = new List<IAppEvent>();
            foreach(var item in Assembly.GetExecutingAssembly().GetTypes().Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface))
            {
                IAppEvent handler = (IAppEvent)Activator.CreateInstance(item);
                result.Add(handler);
            }

            return result.Where(i => i.EvenType == eventType);
        }
    }
}