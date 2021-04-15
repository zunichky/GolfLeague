using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Threading.Tasks;

namespace TrunkSlammers.Shared
{
    public class Player
    {
        public Player()
        { }

        public Player(Player player)
        {
            Handicap = player.Handicap;
            UserId = player.UserId;
            UserInformation = player.UserInformation;
            UserInformationId = player.UserInformationId;
        }
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public int Handicap { get; set; }

        [ForeignKey("LeagueId")]
        public int LeagueId { get; set; }

        public int UserInformationId { get; set; }
        [ForeignKey("UserInformationId")]
        public virtual UserInformation UserInformation { get; set; }
    }
}
