using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrunkSlammers.Shared;

namespace TrunkSlammers.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Team> GetAllTeams()
        {
            var rng = new Random();
            var x = Enumerable.Range(1, 5).Select(index => new Team
            {
                Name = "Team: " + index.ToString(),
                CaptainId = index
            }).ToArray();
            return x;

            //return (await employeeRepository.GetEmployees()).ToList();
        }
    }
}
