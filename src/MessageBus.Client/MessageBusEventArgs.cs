using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBus.Client
{
    public class MessageBusEventArgs : EventArgs
    {
        public string Message { get; set; }
    }

}
