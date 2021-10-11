using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EventSourcing
{

    public class InMemoryEventStore : IEventStore
    {
        public InMemoryEventStore()
        {

        }

        private const string Apply = "Apply";
        private readonly IList<ISnapshot> snapshots = new List<ISnapshot>();
        public List<Event> Events { get; private set; }

        public void Init()
        {
            Events = new List<Event>();
        }

        public void AddSnapshot(ISnapshot snapshot)
        {
            snapshots.Add(snapshot);
        }

        public bool Store<TStream>(TStream aggregate) where TStream : IAggregate
        {
            //TODO Add applying events for all projections
            var events = aggregate.DequeueUncommittedEvents();
            var initialVersion = aggregate.Version - (ulong)events.Count();

            foreach (var @event in events)
            {
                AppendEvent<TStream>(aggregate.Id, @event, initialVersion++);
            }

            snapshots
                .FirstOrDefault(snapshot => snapshot.Handles == typeof(TStream))?
                .Handle(aggregate);

            return true;
        }


        public bool AppendEvent<TStream>(Guid entityId, IEventData @event, ulong? expectedVersion = null)
        {
            if (Events.Exists(ev => ev.EntityId == entityId && ev.Version == expectedVersion))
            {
                return false;
            }
            else
            {
                var newEvent = new Event()
                {
                    Id = Guid.NewGuid(),
                    Data = @event,
                    Created = DateTimeOffset.UtcNow,
                    Type = @event.GetType().ToString(),
                    StreamType = typeof(TStream).FullName,
                    Version = expectedVersion == null ? 0 : expectedVersion.Value,
                    EntityId = entityId 
                };

                Events.Add(newEvent);

                return true;
            }
        }

        public T GetStreamState<T>(Guid entityId)
        {
            return default(T);
        }

        public List<IEventData> GetEvents(Guid entityId)
        {
            return Events.Where(ev => ev.EntityId == entityId).OrderBy(ev => ev.Version).Select(ev => ev.Data).ToList();
        }

        public T AggregateStream<T>(Guid entityId, long? atStreamVersion = null, DateTime? atTimestamp = null) where T : notnull
        {
            var aggregate = (T)Activator.CreateInstance(typeof(T), true)!;

            var events = GetEvents(entityId);
            var version = 0;

            foreach (var @event in events)
            {                
                aggregate.InvokeIfExists(Apply, @event);
                aggregate.SetIfExists(nameof(IAggregate.Version), (ulong)++version);
            }

            return aggregate;
        }
    }

}
