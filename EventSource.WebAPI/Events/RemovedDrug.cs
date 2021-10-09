using System;



namespace EventSourcing
{
    public class RemovedDrug : IEventData
    {
        public Guid Id { get; set; }
    }

}
