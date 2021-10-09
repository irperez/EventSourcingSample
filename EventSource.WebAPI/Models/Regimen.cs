using System;
using System.Collections.Generic;
using System.Linq;



namespace EventSourcing
{

    public partial class Regimen : IAggregate
    {
        public Guid Id { get; protected set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public long Version { get; protected set; }

        public List<Drug> Drugs { get; set; }

        public void Enqueue(IEventData @event)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IEventData> IAggregate.DequeueUncommittedEvents()
        {
            throw new NotImplementedException();
        }
    }

}
