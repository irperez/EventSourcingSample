using System;



namespace EventSourcing
{
    public class AddedDrug : IEventData
    {
        public Guid Id { get; set; }
        public string GenericId { get; set; }
        public double Dose { get; set; }
    }

}
