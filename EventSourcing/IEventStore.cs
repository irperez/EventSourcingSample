using System;



namespace EventSourcing
{
    public interface IEventStore
    {
        void Init();

        bool AppendEvent<TStream>(Guid EntityId, IEventData @event, ulong? expectedVersion = null);

        
    }

}
