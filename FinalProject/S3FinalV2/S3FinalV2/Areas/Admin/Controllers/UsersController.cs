    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using S3FinalV2.Models;

    namespace S3FinalV2.Areas.Admin.Controllers
    {
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public class UsersController : Controller
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly string[] _availableRoles = { "Admin", "Mechanic", "Customer" };

            public UsersController(UserManager<ApplicationUser> userManager)
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
                await ChangeUserRole(id, "Mechanic");
                return RedirectToAction("Index");
            }

            public async Task<IActionResult> AddCustomer(string id)
            {
                await ChangeUserRole(id, "Customer");
                return RedirectToAction("Index");
            }

            public async Task<IActionResult> AddAdmin(string id)
            {
                await ChangeUserRole(id, "Admin");
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

            private async Task ChangeUserRole(string userId, string newRole)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var currentRoles = await _userManager.GetRolesAsync(user);
                
                    if (currentRoles.Count > 0)
                    {
                        await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    }
                
                    await _userManager.AddToRoleAsync(user, newRole);
                }
            }
        }

        public class UserRolesViewModel
        {
            public string Id { get; set; }
            public string Email { get; set; } = string.Empty;
            public IList<string> Roles { get; set; } = new List<string>();
        }
    }