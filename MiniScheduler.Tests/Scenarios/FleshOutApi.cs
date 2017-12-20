using MiniScheduler.Config;
using NUnit.Framework;

namespace MiniScheduler.Tests.Scenarios
{
    [TestFixture]
    public class FleshOutApi : FixtureBase
    {
        [Test]
        public void CanStartIt()
        {
            Scheduler.Configure("test").Delete();
        }
    }
}