using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrunkSlammers.Shared;

namespace TrunkSlammers.Server.Data.Players
{
    public interface IPlayer
    {
        abstract Player GetPlayer(int id);
        abstract List<Player> GetPlayer(string userId);
    }

    public class DbPlayer : IPlayer
    {
        private readonly ApplicationDbContext _db;
        public DbPlayer(ApplicationDbContext context)
        {
            _db = context;
        }
        public Player GetPlayer(int id)
        {
            return _db.Players.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Player> GetPlayer(string userId)
        {
            return _db.Players.Where(x => x.UserId == userId).ToList();
        }
    }
}
