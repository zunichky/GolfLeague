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
        private readonly IDataManager _data;

        public LeagueController(UserManager<ApplicationUser> userManager,
            IDataManager data)
        {
            _userManager = userManager;
            _data = data;
        }

        [HttpGet]
        public async Task<ActionResult<List<League>>> GetAllLeagues()
        {
            var user = await getApplicationUser();
            if (user != null)
            {
                var returnLeague = await _data.Leagues.GetLeagues(user.Id);
                return Ok(returnLeague);
            }
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<League>> GetLeague(int id)
        {
            var user = await getApplicationUser();
            if (user is null || !_data.Leagues.IsPlayerInLeague(id, user.Id))
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            return Ok(await _data.Leagues.GetLeague(id));
        }

        [HttpPost]
        public async Task<ActionResult<League>> CreateLeague(League newLeague)
        {
            var user = await getApplicationUser();
            if (user is null)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            newLeague.Players.Add(new Player()
            {
                UserId = user.Id,
                UserInformationId = user.UserInformationId
            });

            League createdLeague = await _data.Leagues.CreateLeague(newLeague);
            return Ok(createdLeague);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteLeague(League league)
        {
            var user = await getApplicationUser();
            if (user is null || !_data.Leagues.IsPlayerInLeague(league.Id, user.Id))
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            await _data.Leagues.DeleteLeague(league);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> EditLeagueName(League leagueToEdit)
        {
            var user = await getApplicationUser();
            if (user is null || !_data.Leagues.IsPlayerInLeague(leagueToEdit.Id, user.Id))
                return StatusCode(StatusCodes.Status401Unauthorized);

            await _data.Leagues.EditLeagueName(leagueToEdit.Id, leagueToEdit.Name);

            return Ok();
        }

        private async Task<ApplicationUser> getApplicationUser()
        {
            ApplicationUser returnUser = null;
            if (User is not null)
            {
                returnUser = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            return returnUser;
        }
    }
}
