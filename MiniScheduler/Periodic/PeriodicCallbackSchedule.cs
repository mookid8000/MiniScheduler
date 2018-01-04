using System;
using MiniScheduler.Catalog;

namespace MiniScheduler.Periodic
{
    public class PeriodicCallbackSchedule : Schedule
    {
        ScheduleState _state;

        public PeriodicCallbackSchedule(TimeSpan interval, Action callback)
        {
            
        }

        public override ScheduleState State
        {
            get { return _state; }
            set { _state = value; }
        }
    }
}