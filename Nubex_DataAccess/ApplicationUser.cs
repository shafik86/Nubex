
using Microsoft.AspNetCore.Identity;

namespace Nubex_DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
