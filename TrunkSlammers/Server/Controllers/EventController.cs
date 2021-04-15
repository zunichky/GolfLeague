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
    public class EventController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IDataManager _data;

        public EventController(UserManager<ApplicationUser> userManager
            ,ApplicationDbContext context
            , IDataManager data)
        {
            _userManager = userManager;
            _db = context;
            _data = data;
        }


        [HttpGet]
        public IEnumerable<Event> GetAllEvents()
        {
            if (User != null) /* The user object is found to be null here. */
            {
                var user = _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                
                if (user.Result != null)
                {
                    /*
                    List<League> leaguesUserIsIn = _db.Leagues.Where(
                    h => h.Players.Any(f => f.UserId == user.Result.Id))
                    .Include(z => z.Players).Include(i => i.Events).ToList();
                    return leaguesUserIsIn;
                    */
                }
            }
            return null;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            try
            {
                //var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var foundEvent = await _db.Events
                    .Include(y => y.Teams)
                    .Where(x => x.Id == id).SingleOrDefaultAsync();

                Player x = _data.Players.GetPlayer(3);


                if (foundEvent is null)
                    return NotFound();

                return Ok(foundEvent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("{leagueId:int}")]
        public async Task<IActionResult> CreateEvent(int leagueId, Event newEvent)
        {
            //TODO: Secure with userId
            var user = _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            League league = _db.Leagues.Find(leagueId);
            league.Events.Add( newEvent);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(Event curEvent)
        {
            //TODO: Secure with userId
            _db.Events.Remove(curEvent);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EditEventName(Event eventToEdit)
        {
            //TODO: Secure with userId
            var foundEvent = _db.Events.FirstOrDefault(x => x.Id == eventToEdit.Id);
            if (foundEvent is not null)
            {
                foundEvent.Name = eventToEdit.Name;
                await _db.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
