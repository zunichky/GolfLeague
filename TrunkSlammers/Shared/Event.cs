using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrunkSlammers.Shared
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Location { get; set; }
        public Draft Draft { get; set; }
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
        [ForeignKey("LeagueId")]
        public int LeagueId { get; set; }
    }
}
