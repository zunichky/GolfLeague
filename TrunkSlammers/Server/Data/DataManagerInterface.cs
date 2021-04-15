using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrunkSlammers.Server.Data;
using TrunkSlammers.Shared;

namespace TrunkSlammers.Server.Data
{
    public interface IDataManager
    {
        abstract Players.IPlayer Players { get; }
        abstract Leagues.ILeague Leagues{ get; }
    }

    public class DatabaseData : IDataManager
    {
        private Players.DbPlayer _players;
        private Leagues.DbLeague _leagues;

        public DatabaseData(ApplicationDbContext context)
        {   
            _players = new Players.DbPlayer(context);
            _leagues = new Leagues.DbLeague(context);
        }

        public Players.IPlayer Players
        {
            get => _players;
        }
        public Leagues.ILeague Leagues
        {
            get => _leagues;
        }
    }
}
