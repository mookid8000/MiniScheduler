using MiniScheduler.Config;
using MiniScheduler.Logging;

namespace MiniScheduler.Persistence.SqlServer
{
    public class SqlServerScheduleStore : IScheduleStore, IInitializable
    {
        readonly ILog _log = LogProvider.For<SqlServerScheduleStore>();
        readonly string _connectionString;
        readonly string _schemaName;
        readonly string _tableName;

        public SqlServerScheduleStore(string connectionString, string schemaName, string tableName)
        {
            _connectionString = connectionString;
            _schemaName = schemaName;
            _tableName = tableName;
        }

        public void Initialize()
        {
            _log.Info("Initializing SQL Server schedule store");

            //EnsureTableIsCreated();
        }

        public void Delete(string schedulerId)
        {
            
        }
    }
}