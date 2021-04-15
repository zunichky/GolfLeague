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
    public class PlayerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public PlayerController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _db = context;
        }
    }
}
