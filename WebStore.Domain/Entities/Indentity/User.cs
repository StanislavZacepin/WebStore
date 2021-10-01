using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.Entities.Indentity
{
   public class User : IdentityUser
    {
        public const string Administrator = "Admin";

        public const string DefaultAdminPassword = "AdPass_123";
    }
}
