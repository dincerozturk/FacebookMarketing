using System;
using Microsoft.AspNetCore.Identity;

namespace FM.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string roleName) : base(roleName)
        {

        }
    }
}