using System;
using System.Collections.Generic;

namespace EventSourcing
{
    public class InMemoryProjectionStore<T> : IProjectionHandler
    {
        public Type Handles => typeof(T);

        public void Handle(IAggregate<Guid> projection)
        {
            // Store to Durable store
            // In our example to Cosmos DB
            if (ProjectionData.ContainsKey(projection.Id))
            {
                ProjectionData[projection.Id].Add(projection);
            }
            else
            {
                var list = new List<IAggregate<Guid>>();
                list.Add(projection);
                ProjectionData.Add(projection.Id, list);
            }
        }

        public Dictionary<Guid, List<IAggregate<Guid>>> ProjectionData { get; protected set; } = new Dictionary<Guid, List<IAggregate<Guid>>>();
    }

}
