using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nubex.Service.IService;
using Nubex_Common;
using Nubex_DataAccess.Data;

namespace Nubex.Service
{
    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public DbInitializer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Account)).GetAwaiter().GetResult();
                    //_roleManager.CreateAsync(new IdentityRole(SD.Role_Cara)).GetAwaiter().GetResult();
                }
                else
                {
                    return;
                }

                IdentityUser userAdmin = new()
                {
                    UserName = "superadmin@cara.com.my",
                    Email = "superadmin@cara.com.my",
                    EmailConfirmed = true
                };
                IdentityUser user = new()
                {
                    UserName = "user@cara.com.my",
                    Email = "user@cara.com.my",
                    EmailConfirmed = true
                };

                _userManager.CreateAsync(userAdmin, "Admin123@").GetAwaiter().GetResult();
                _userManager.CreateAsync(user, "User123@").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(userAdmin, SD.Role_Admin).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, SD.Role_Customer).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
