using EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventSource.WebAPI.Models
{
    public partial class RegimenIndex
    {
        //public void When(IEventData @event)
        //{
        //    switch (@event)
        //    {
        //        case AddedRecord ad:
        //            Apply(ad);
        //            break;
        //        case AddedDrug ad:
        //            Apply(ad);
        //            break;
        //        default:
        //            //ignore; do nothing
        //            break;
        //    }
        //}

        public void Apply(AddedRecord @event)
        {
            Id = @event.Id;
            Name = @event.Name;
        }
        public void Apply(AddedDrug @event)
        {
            Drugs.Add(new Drug() { Id = @event.Id, Dose = @event.Dose, GenericId = @event.GenericId });
        }

        public void Apply(UpdatedDrug @event)
        {
            var result = Drugs.Where(drg => drg.Id == @event.Id).FirstOrDefault();
            if (result != null)
            {
                result.Dose = @event.Dose;
                result.GenericId = @event.GenericId;
            }
        }

        public void Apply(RemovedDrug @event)
        {
            int idx = -1;
            for (int i = 0; i < Drugs.Count - 1; i++)
            {
                if (Drugs[i].Id == @event.Id)
                {
                    idx = i;
                    break;
                }
            }

            if (idx > -1)
            {
                Drugs.RemoveAt(idx);
            }
        }

        //private void UpdateIndex()
        //{
        //    foreach (var item in Drugs)
        //    {
        //        DrugIndex += "|" + item.GenericId;
        //    }
        //}

        [JsonIgnore]
        private readonly List<IEventData> uncommittedEvents = new List<IEventData>();



    }
}
