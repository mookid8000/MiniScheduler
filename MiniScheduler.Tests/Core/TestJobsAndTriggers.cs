using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MiniScheduler.Tests.Core
{
    [TestFixture]
    public class TestJobsAndTriggers : FixtureBase
    {
        [Test]
        public void GreatName()
        {
            var job = new Job("bimse", async () =>
            {
                await Console.Out.WriteAsync("yay jeg kører");
            });
        }
    }

    public class Job
    {
        readonly Func<Task> _execute;

        public string Id { get; }

        public Job(string id, Func<Task> execute)
        {
            _execute = execute;
            Id = id;
        }

        public Task Execute() => _execute();
    }

    public abstract class Trigger
    {
        

    }

    public class FixedIntervalTrigger : Trigger
    {
        public TimeSpan Interval { get; }

        public FixedIntervalTrigger(TimeSpan interval)
        {
            Interval = interval;
        }
    }

    public class SpecificTimeOfDayTrigger : Trigger
    {
        public TimeSpan TimeOfDay { get; }

        public SpecificTimeOfDayTrigger(TimeSpan timeOfDay, DateTimeOffset first)
        {
            TimeOfDay = timeOfDay;
        }
    }
}