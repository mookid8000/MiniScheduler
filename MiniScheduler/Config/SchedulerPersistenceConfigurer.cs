using MiniScheduler.Persistence.InMem;
using MiniScheduler.Persistence.SqlServer;

namespace MiniScheduler.Config
{
    public class SchedulerPersistenceConfigurer
    {
        readonly string _schedulerId;

        public SchedulerPersistenceConfigurer(string schedulerId)
        {
            _schedulerId = schedulerId;
        }

        public SchedulerConfigurer StoreInMemory() => new SchedulerConfigurer(_schedulerId, new InMemScheduleStore());

        public SchedulerConfigurer StoreInSqlServer(string connectionString, string tableName)
        {
            return new SchedulerConfigurer(_schedulerId, new SqlServerScheduleStore(connectionString, "dbo", tableName));
        }
    }
}