using System;
using System.Threading;
using System.Threading.Tasks;

namespace Utils
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

        public static Task StartPeriodic(this TaskFactory source, Action action, int intervalInMilliseconds, CancellationToken cancellationToken, int maxIteration = -1)
        {
            return PeriodicTaskFactory.Start(action, intervalInMilliseconds, maxIterations: maxIteration, cancelToken: cancellationToken);
        }
    }
}
