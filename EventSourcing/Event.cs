using System;



namespace EventSourcing
{
    public class Event
    {
        public Guid Id { get; set; }
        public IEventData Data { get; set; }
        public Guid EntityId { get; set; }
        public string Type { get; set; }
        public string StreamType { get; set; }
        public ulong Version { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public string PartitionKey => StreamType;        
    }

}
