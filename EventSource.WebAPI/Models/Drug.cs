using System;



namespace EventSourcing
{
    public class Drug
    {
        public Guid Id { get; set; }
        public string GenericId { get; set; }
        public double Dose { get; set; }
    }

}
