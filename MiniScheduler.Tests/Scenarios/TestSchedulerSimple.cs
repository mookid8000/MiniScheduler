using NUnit.Framework;

namespace MiniScheduler.Tests.Scenarios
{
    [TestFixture]
    public class TestSchedulerSimple : FixtureBase
    {
        Scheduler _scheduler;

        protected override void SetUp()
        {
            _scheduler = new Scheduler();

            Using(_scheduler);
        }

        [Test]
        public void CanStartIt()
        {
            _scheduler.Start();
        }
    }
}