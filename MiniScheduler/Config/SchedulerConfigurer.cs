using System;
using System.Linq;
using Injectionist;
using MiniScheduler.Catalog;
using MiniScheduler.Identification;
using MiniScheduler.Logging;
using MiniScheduler.Persistence;

namespace MiniScheduler.Config
{
    public class SchedulerConfigurer
    {
        readonly Injectionist.Injectionist _injectionist = new Injectionist.Injectionist();
        readonly ILog _logger = LogProvider.For<SchedulerConfigurer>();
        readonly ScheduleCatalog _catalog = new ScheduleCatalog();
        readonly string _schedulerId;

        public SchedulerConfigurer(string schedulerId, IScheduleStore scheduleStore)
        {
            _logger.DebugFormat("Configuring scheduler with ID {SchedulerId}", _schedulerId);
            _schedulerId = schedulerId;
            _injectionist.Register(c => new SchedulerId(schedulerId));
            _injectionist.Register(c => _catalog);
            _injectionist.Register(c => scheduleStore);
        }

        /// <summary>
        /// Starts the scheduler
        /// </summary>
        public Scheduler Start()
        {
            _logger.InfoFormat("Starting scheduler with ID {SchedulerId}", _schedulerId);
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
            _logger.InfoFormat("Deleting scheduler with ID {SchedulerId}", _schedulerId);
            RegisterDefaults();

            var result = _injectionist.Get<IScheduleStore>();

            Initialize(result);

            try
            {
                result.Instance.Delete(_schedulerId);
            }
            finally
            {
                var disposables = result.TrackedInstances.OfType<IDisposable>().ToList();

                foreach (var disposable in disposables)
                {
                    disposable.Dispose();
                }
            }
        }

        internal void Add(string id, Schedule schedule) => _catalog.Add(id, schedule);

        void RegisterDefaults()
        {
            if (_injectionist.Has<Scheduler>()) return;

            _injectionist.Register(context =>
            {
                var scheduler = new Scheduler();
                HandleDisposal(scheduler, context);
                return scheduler;
            });
        }

        static void Initialize<T>(ResolutionResult<T> result)
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