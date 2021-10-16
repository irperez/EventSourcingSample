using EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.WebAPI.Models
{
    public partial class RegimenIndex : Aggregate<Guid>
    {
        public string Name { get; protected set; }
        public string DrugIndex => GetIndex();
        
        private List<Drug> Drugs { get; set; } = new();

        private string GetIndex()
        {
            var builder = new StringBuilder();
            foreach (var item in Drugs)
            {
                builder.Append("|" + item.GenericId);
            }

            return builder.ToString();
        }
    }
}
