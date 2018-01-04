using System;
using System.Collections.Generic;

namespace MiniScheduler.Catalog
{
    public class ScheduleCatalog
    {
        readonly Dictionary<string, Schedule> _schedules = new Dictionary<string, Schedule>();

        public void Add(string id, Schedule schedule)
        {
            _schedules[id] = schedule ?? throw new ArgumentNullException(nameof(schedule), "Please don't add null");
        }

    }
}