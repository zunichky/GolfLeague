using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrunkSlammers.Shared;

namespace TrunkSlammers.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserInformationId { get; set; }
        [ForeignKey("UserInformationId")]
        public virtual UserInformation UserInformation { get; set; } = new UserInformation();
    }
}
