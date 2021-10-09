using System;



namespace EventSourcing
{
    public class AddedRecord : IEventData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }

}
