using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventSourcing
{
    public partial class Regimen
    {
        [JsonIgnore]
        private readonly List<object> uncommittedEvents = new List<object>();
        IEnumerable<object> DequeueUncommittedEvents()
        {
            var dequeuedEvents = uncommittedEvents.ToList();

            uncommittedEvents.Clear();

            return dequeuedEvents;
        }

        protected void Enqueue(object @event)
        {
            Version++;
            uncommittedEvents.Add(@event);
        }

        public void Apply(AddedRecord @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            Value = @event.Value;
        }

        public void Apply(AddedDrug @event)
        {
            if (Drugs == null)
            {
                Drugs = new List<Drug>();
            }

            Drugs.Add(new Drug() { Id = @event.Id, GenericId = @event.GenericId, Dose = @event.Dose });
        }

        public void Apply(UpdatedDrug @event)
        {
            var actualDrg = Drugs.Where(drg => drg.Id == @event.Id).First();
            actualDrg.Dose = @event.Dose;
            actualDrg.GenericId = @event.GenericId;
        }

        public void Apply(RemovedDrug @event)
        {
            int idx = 0;
            foreach (var item in Drugs)
            {
                if (item.Id == @event.Id)
                {
                    break;
                }
                idx += 1;
            }
            Drugs.RemoveAt(idx);
        }
    }
}
