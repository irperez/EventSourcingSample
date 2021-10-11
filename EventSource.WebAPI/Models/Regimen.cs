using System;
using System.Collections.Generic;
using System.Linq;



namespace EventSourcing
{
    public partial class Regimen : Aggregate<Guid>
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public List<Drug> Drugs { get; set; }
    }
}
