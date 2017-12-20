using System;
using System.Diagnostics;

namespace MiniScheduler.Identification
{
    public class SchedulerId : IEquatable<SchedulerId>
    {
        public string Id { get; }

        [DebuggerStepThrough]
        public SchedulerId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"Scheduler ID '{id}' cannot be used - must not be null or whitespace");
            }

            Id = id;
        }

        public bool Equals(SchedulerId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SchedulerId) obj);
        }

        public override int GetHashCode() => (Id != null ? Id.GetHashCode() : 0);

        public static bool operator ==(SchedulerId left, SchedulerId right) => Equals(left, right);

        public static bool operator !=(SchedulerId left, SchedulerId right) => !Equals(left, right);

        public override string ToString() => $"SchedulerId('{Id}')";
    }
}