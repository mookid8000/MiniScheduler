using System;
using System.Collections.Generic;
using System.Threading;
using MiniScheduler.Config;
using MiniScheduler.Periodic;
using NUnit.Framework;

namespace MiniScheduler.Tests.Scenarios
{
    [TestFixture]
    public class FleshOutApi : FixtureBase
    {
        [Test]
        public void CanStartIt()
        {
            var invocationTimes = new List<DateTimeOffset>();

            var configurer = Scheduler.Configure("test")
                .StoreInMemory()
                .AddSimple("first_timer", TimeSpan.FromSeconds(1), () =>
                {
                    var time = DateTimeOffset.Now;
                    Console.WriteLine($"callback: {time}");
                    invocationTimes.Add(time);
                });

            configurer.Delete();

            using (configurer.Start())
            {
                Thread.Sleep(4000);
            }

            Console.WriteLine(string.Join(Environment.NewLine, invocationTimes));

            Assert.That(invocationTimes.Count, Is.GreaterThan(3));
        }

        [Test]
        public void CanStartIt_SqlServer()
        {
            var invocationTimes = new List<DateTimeOffset>();

            var configurer = Scheduler.Configure("test")
                .StoreInSqlServer("server=.; database=MiniScheduler; trusted_connection=true", "Schedules")
                .AddSimple("first_timer", TimeSpan.FromSeconds(1), () =>
                {
                    var time = DateTimeOffset.Now;
                    Console.WriteLine($"callback: {time}");
                    invocationTimes.Add(time);
                });

            configurer.Delete();

            using (configurer.Start())
            {
                Thread.Sleep(4000);
            }

            Console.WriteLine(string.Join(Environment.NewLine, invocationTimes));

            Assert.That(invocationTimes.Count, Is.GreaterThan(3));
        }
    }
}