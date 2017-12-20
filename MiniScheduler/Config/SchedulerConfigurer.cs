using System;
using System.Linq;
using Injectionist;
using MiniScheduler.Identification;

namespace MiniScheduler.Config
{
    public class SchedulerConfigurer
    {
        readonly Injectionist.Injectionist _injectionist = new Injectionist.Injectionist();

        public SchedulerConfigurer(string schedulerId) => _injectionist.Register(c => new SchedulerId(schedulerId));

        /// <summary>
        /// Starts the scheduler
        /// </summary>
        public Scheduler Start()
        {
            RegisterDefaults();

            var result = _injectionist.Get<Scheduler>();

            Initialize(result);

            return result.Instance;
        }

        /// <summary>
        /// Deletes all state associated with this particular scheduler instance, leaving no trace of it.
        /// Warning: No questions asked, it will be entirely removed at once!
        /// </summary>
        public void Delete()
        {
                
        }

        void RegisterDefaults()
        {
            _injectionist.Register(context =>
            {
                var scheduler = new Scheduler();
                HandleDisposal(scheduler, context);
                return scheduler;
            });
        }

        static void Initialize(ResolutionResult<Scheduler> result)
        {
            var initializables = result.TrackedInstances.OfType<IInitializable>().ToList();

            foreach (var initializable in initializables)
            {
                initializable.Initialize();
            }
        }

        static void HandleDisposal(Scheduler scheduler, IResolutionContext context)
        {
            scheduler.Disposed += () =>
            {
                var disposables = context.GetTrackedInstancesOf<IDisposable>().Reverse().ToList();

                foreach (var disposable in disposables)
                {
                    disposable.Dispose();
                }
            };
        }
    }
}