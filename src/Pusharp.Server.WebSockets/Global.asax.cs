using System;
using System.Web;
using Pusharp.Server.WebSockets.AppEvents;

namespace Pusharp.Server.WebSockets
{
    public class Global : HttpApplication
    {
        
        protected void Application_Start(object sender, EventArgs e)
        {

            AppEventManager.ExecuteHandler(AppEventType.App_Start);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}