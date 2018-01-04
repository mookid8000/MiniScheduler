using System;
using MiniScheduler.Config;

namespace MiniScheduler
{
    /// <summary>
    /// Main scheduler class
    /// </summary>
    public class Scheduler : IDisposable
    {
        public static SchedulerPersistenceConfigurer Configure(string schedulerId) => new SchedulerPersistenceConfigurer(schedulerId);

        internal event Action Disposed;

        bool _disposing;
        bool _disposed;

        /// <summary>
        /// Releases the resources
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;
            if (_disposing) return;

            _disposing = true;

            try
            {
                Disposed?.Invoke();
            }
            finally
            {
                _disposing = false;
                _disposed = true;
            }
        }
    }
}
