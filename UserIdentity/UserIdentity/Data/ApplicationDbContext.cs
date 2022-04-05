using UserIdentity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, Guid, 
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IApplicationDbContext
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}
