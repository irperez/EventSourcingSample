using System;
using System.Collections.Generic;

namespace EventSourcing
{
    public interface IAggregate
    {
        Guid Id { get; }
        long Version { get; }

        IEnumerable<IEventData> DequeueUncommittedEvents();
        void Enqueue(IEventData @event);
    }

}
