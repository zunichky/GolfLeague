using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrunkSlammers.Shared
{
    public class Draft
    {
        public string Name { get; set; }
        public int DraftId { get; set; }
        public int EventId { get; set; }
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public DraftStatus Status { get; set; }

        public enum DraftStatus
        {
            Created,
            Ongoing,
            Closed
        }
    }
}
