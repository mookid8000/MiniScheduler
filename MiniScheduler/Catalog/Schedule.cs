namespace MiniScheduler.Catalog
{
    public abstract class Schedule
    {
        public virtual ScheduleState State { get; set; }
    }
}