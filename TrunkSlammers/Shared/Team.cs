using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrunkSlammers.Shared
{
    public class Team
    {
        public Team() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaptainId { get; set; }
        public ICollection<Player> Members { get; set; } = new List<Player>();
    }
}
