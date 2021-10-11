using System;
using System.Collections.Generic;

namespace EventSourcing
{
    public class InMemorySnapshotStore<T> : ISnapshot
    {
        public Type Handles => typeof(T);

        public void Handle(IAggregate<Guid> aggregate)
        {
            // Store to Durable store
            // In our example to Cosmos DB
            if (SnapshotData.ContainsKey(aggregate.Id))
            {
                SnapshotData[aggregate.Id].Add(aggregate);
            }
            else
            {
                var list = new List<IAggregate<Guid>>();
                list.Add(aggregate);
                SnapshotData.Add(aggregate.Id, list);
            }
        }

        public Dictionary<Guid, List<IAggregate<Guid>>> SnapshotData { get; protected set; } = new Dictionary<Guid, List<IAggregate<Guid>>>();
    }

}
