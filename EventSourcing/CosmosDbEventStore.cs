using System;



namespace EventSourcing
{
    public class CosmosDbEventStore : IEventStore
    {
        public bool AppendEvent<TStream>(Guid EntityId, IEventData @event, long? expectedVersion = null)
        {
            throw new NotImplementedException();
        }

        public bool AppendEvent<TStream>(Guid EntityId, IEventData @event, ulong? expectedVersion = null)
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }
    }

}
