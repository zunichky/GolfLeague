using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrunkSlammers.Shared;
using TrunkSlammers.Server.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TrunkSlammers.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TrunkSlammers.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LeagueController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public LeagueController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Team> GetAllTeams()
        {
            List<Team> y = new List<Team>();
            y.Add(new Team());
            return y;

            //return (await employeeRepository.GetEmployees()).ToList();
        }

        [HttpGet]
        public IEnumerable<League> GetAllLeagues()
        {
            if (User != null) /* The user object is found to be null here. */
            {
                var user = _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (user.Result != null)
                {
                    List<League> leaguesUserIsIn = _context.Leagues.Where(
                    h => h.Players.Any(f => f.UserId == user.Result.Id))
                    .Include(z => z.Players).Include(i => i.Events).ToList();
                    return leaguesUserIsIn;
                }
            }
            return null;
        }

        [HttpGet("{id:int}")]
        public League GetLeague(int id)
        {
            if (User != null) /* The user object is found to be null here. */
            {
                var user = _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (user.Result != null)
                {
                    foreach (var league in FakeData.leagues)
                    {
                        if (league.Id == id)
                        {
                            //var matches = league.Players.Where(p => p.Id == user.Result.Id);
                            foreach (Player player in league.Players)
                            {
                                if (player.UserId == user.Result.Id)
                                {
                                    return league;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeague(League newLeague)
        {
            _context.Leagues.Add(newLeague);
            await _context.SaveChangesAsync();
            return Ok();
        }

        /*
        [HttpGet("{id:string}")]
        public async Task<ActionResult<UserInformation>> GetUser(string id)
        {
            UserInformation userInfo = new UserInformation();
            var user = _userManager.FindByIdAsync(id);
            if (user.Result != null)
            {
                userInfo.FirstName = user.Result.FirstName;
                userInfo.LastName = user.Result.LastName;
                userInfo.Hand = user.Result.Hand;
            }
            return Ok(userInfo);
        }
        */
            [HttpGet("{id:int}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            try
            {
                return Ok(new Team());
                /*
                var result = await employeeRepository.GetEmployee(id);

                if (result == null) return NotFound();

                return result;
                */
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
