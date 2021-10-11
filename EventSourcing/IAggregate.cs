using System;
using System.Collections.Generic;

namespace EventSourcing
{

    public interface IAggregate<out T> : IProjection
    {
        T Id { get; }
        ulong Version { get; }

        IEventData[] DequeueUncommittedEvents();
        void Enqueue(IEventData @event);
    }

    public interface IAggregate : IAggregate<Guid>
    {
        
    }

    public abstract class Aggregate : Aggregate<Guid>, IAggregate
    {

    }

    public abstract class Aggregate<T> : IAggregate<T> where T : notnull
    {
        public T Id { get; protected set; }

        public ulong Version { get; protected set; }

        [NonSerialized] 
        private readonly Queue<IEventData> uncommittedEvents = new();

        public IEventData[] DequeueUncommittedEvents()
        {
            var dequeuedEvents = uncommittedEvents.ToArray();
            uncommittedEvents.Clear();

            return dequeuedEvents;
        }

        public void Enqueue(IEventData @event)
        {
            uncommittedEvents.Enqueue(@event);
        }
        
        //public virtual void When(IEventData @event) { }        
    }

}
