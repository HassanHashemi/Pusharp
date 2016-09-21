using System;
using System.Threading.Tasks;

namespace Pusharp.Server.WebSockets
{
    public static class TaskExtensions
    {
        public static async void Forget(this Task source)
        {
            try
            {
                await source.ConfigureAwait(false);
            }
            catch (Exception)
            {

            }
        }

        public static Task CreateFaultedTask(this TaskFactory source, Exception ex)
        {
            return CreateFaultedTask<object>(null, ex);
        }

        public static Task<TResult> CreateFaultedTask<TResult>(this TaskFactory source, Exception ex)
        {
            TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>();
            tcs.SetException(ex);
            return tcs.Task;
        }
    }
}