using System.Collections.Concurrent;
using MiniScheduler.Logging;

namespace MiniScheduler.Persistence.InMem
{
    public class InMemScheduleStore : IScheduleStore
    {
        readonly ConcurrentDictionary<string, ScheduleContainer> _data = new ConcurrentDictionary<string, ScheduleContainer>();

        readonly ILog _log = LogProvider.For<InMemScheduleStore>();



        public void Delete(string schedulerId)
        {
            GetContainerFor(schedulerId).Clear();
        }

        ScheduleContainer GetContainerFor(string schedulerId) => _data.GetOrAdd(schedulerId, _ => new ScheduleContainer());

        class ScheduleContainer
        {
            readonly ConcurrentQueue<string> _schedules = new ConcurrentQueue<string>();

            public void Clear()
            {
                while (_schedules.TryDequeue(out var _)) ;
            }
        }
    }
}