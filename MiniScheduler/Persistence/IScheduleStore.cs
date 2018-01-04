namespace MiniScheduler.Persistence
{
    public interface IScheduleStore
    {
        void Delete(string schedulerId);
    }
}