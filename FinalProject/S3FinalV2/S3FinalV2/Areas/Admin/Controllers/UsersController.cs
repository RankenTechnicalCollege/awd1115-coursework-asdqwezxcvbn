using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace S3FinalV2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var model = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                model.Add(new UserRolesViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = roles
                });
            }

            return View(model);
        }

        public async Task<IActionResult> AddMechanic(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
                await _userManager.AddToRoleAsync(user, "Mechanic");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddCustomer(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
                await _userManager.AddToRoleAsync(user, "Customer");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
                await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Index");
        }
    }

    public class UserRolesViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}