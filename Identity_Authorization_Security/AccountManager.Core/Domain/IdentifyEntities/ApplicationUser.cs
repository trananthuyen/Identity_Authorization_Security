using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Core.Domain.IdentifyEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? AccountName { get; set; }
    }
}
