using MessageBus.Client;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Pusharp.Server.WebSockets
{
    public class MessageBroker
    {
        private static MessageBroker _current;


        private readonly int MAX_BROKER_THREAD_COUNT = Environment.ProcessorCount * 4;
        private readonly ActionBlock<Tuple<string, WebSocket>> _worker;
        private  IMessageBusClient _bus;
        
        private IMessageBusClient Bus
        {
            get
            {
                var busClient = _bus ?? (_bus = new WebSocketMessageBusClient(null));
                Task.Run(async () => await busClient.Start());
                busClient.MessageReceived += BusClient_MessageReceived;
                return busClient;
            }
            set
            {
                _bus = value;
            }
        }

        private void BusClient_MessageReceived(object sender, MessageBusEventArgs e)
        {
            
        }

        public MessageBroker GetCurrentInstance()
        {
            return this.GetCurrentInstance(CancellationToken.None);
        }

        public MessageBroker GetCurrentInstance(CancellationToken token)
        {
            return _current ?? (_current = new MessageBroker(token));
        }

        private void _broker_MessageReceived(object sender, MessageBusEventArgs e)
        {
            throw new NotImplementedException();
        }

        private MessageBroker(CancellationToken cancellationToken)
        {
            _worker = new ActionBlock<Tuple<string, WebSocket>>(
                (tuple) =>
                {
                    this.SendAsync(tuple.Item2, tuple.Item1).Forget();
                }, new ExecutionDataflowBlockOptions()
                    {
                        BoundedCapacity = int.MaxValue,
                        MaxDegreeOfParallelism = MAX_BROKER_THREAD_COUNT,
                        CancellationToken = cancellationToken
                    });
        }

        private MessageBroker() : this(CancellationToken.None) { }
        
        private Task SendAsync(WebSocket socket, string message, int retryCount = 3)
        {
            try
            {
                message = message + "(" + Thread.CurrentThread.ManagedThreadId + ")";
                return socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException && retryCount > 0)
                {
                    Thread.Sleep(30);
                    return SendAsync(socket, message, --retryCount);
                }

                return Task.Factory.CreateFaultedTask(ex);
            }
        }
        
        public void Post(WebSocket socket, string message)
        {
            this._worker.Post(new Tuple<string, WebSocket>(message, socket));
        }
    }
}