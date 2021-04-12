using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Threading.Tasks;

namespace TrunkSlammers.Shared
{
    public class Player
    {
        //static HttpClient client = new HttpClient();
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public int Handicap { get; set; }

        public int UserInformationId { get; set; }
        [ForeignKey("UserInformationId")]
        public virtual UserInformation UserInformation { get; set; }

        /*
        public async Task<UserInformation> GetUserInformation()
        {
            UserInformation userInfo = null;
            HttpResponseMessage response = await client.GetAsync("api/League/GetUser/" + UserId);
            if (response.IsSuccessStatusCode)
            {
                userInfo = await response.Content.ReadAsAsync<UserInformation>();
            }
            return userInfo;
        }
        */
    }
}
