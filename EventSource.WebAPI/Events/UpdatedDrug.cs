using System;



namespace EventSourcing
{
    public class UpdatedDrug : IEventData
    {
        public Guid Id { get; set; }
        public string GenericId { get; set; }
        public double Dose { get; set; }
    }

}
