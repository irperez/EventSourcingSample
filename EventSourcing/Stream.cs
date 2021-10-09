using System;



namespace EventSourcing
{
    public class Stream
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
        
    }

}
