using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrunkSlammers.Shared;
using Microsoft.EntityFrameworkCore;

namespace TrunkSlammers.Server.Data.Leagues
{
    public interface ILeague
    {
        abstract Task<League> GetLeague(int leagueId);
        abstract Task<List<League>> GetLeagues();
        abstract Task<List<League>> GetLeagues(string userId);
        abstract bool IsPlayerInLeague(int leagueId, string userId);
        abstract Task<League> CreateLeague(League league);
        abstract Task DeleteLeague(League league);
        abstract Task EditLeagueName(int leagueId, string name);
    }

    public class DbLeague : ILeague
    {
        private readonly ApplicationDbContext _db;
        public DbLeague(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<League> GetLeague(int leagueId)
        {
            return await _db.Leagues.FindAsync(leagueId);
        }

        public async Task<List<League>> GetLeagues() 
        {
            return await _db.Leagues.Include(z => z.Players).Include(i => i.Events).ToListAsync();
        }
        public async Task<List<League>> GetLeagues(string userId)
        {
            return await _db.Leagues.Where(
                    h => h.Players.Any(f => f.UserId == userId))
                    .Include(z => z.Players).Include(i => i.Events).ToListAsync();
        }

        public bool IsPlayerInLeague(int leagueId, string userId)
        {
            return _db.Leagues.Any(h => 
                   (h.Id == leagueId) 
                && (h.Players.Any(f => f.UserId == userId)));
        }

        public async Task<League> CreateLeague(League league)
        {
            await _db.Leagues.AddAsync(league);
            await _db.SaveChangesAsync();
            return league;
        }

        public async Task DeleteLeague(League league)
        {
            _db.Leagues.Remove(league);
            await _db.SaveChangesAsync();
        }

        public async Task EditLeagueName(int leagueId, string name)
        {
            League league = await GetLeague(leagueId);
            league.Name = name;
            await _db.SaveChangesAsync();
        }
    }
}
