using System;



namespace EventSourcing
{
    public interface IEventStore
    {
        void Init();

        bool AppendEvent<TStream>(Guid EntityId, IEventData @event, long? expectedVersion = null);
    }

}
