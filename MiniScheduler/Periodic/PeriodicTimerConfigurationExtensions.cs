using System;
using MiniScheduler.Config;

namespace MiniScheduler.Periodic
{
    public static class PeriodicTimerConfigurationExtensions
    {
        public static SchedulerConfigurer AddSimple(this SchedulerConfigurer configurer, string id, TimeSpan interval, Action callback)
        {
            configurer.Add(id, new PeriodicCallbackSchedule(interval, callback));

            return configurer;
        }
    }
}