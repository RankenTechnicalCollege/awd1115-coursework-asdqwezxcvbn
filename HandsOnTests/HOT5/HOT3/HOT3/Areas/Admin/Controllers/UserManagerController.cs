using HOT3.Areas.Admin.ViewModel;
using HOT3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HOT3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> _um;
        private readonly RoleManager<IdentityRole> _rm;

        public UserManagerController(UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            _um = um;
            _rm = rm;
        }

        public IActionResult Index()
        {
            var users = _um.Users.ToList();
            return View(users);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName)) return RedirectToAction("Index");
            if (!await _rm.RoleExistsAsync(roleName))
            {
                await _rm.CreateAsync(new IdentityRole(roleName));
            }
            TempData["Message"] = $"Role '{roleName}' created.";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _um.FindByIdAsync(userId);
            if (user != null)
            {
                await _um.DeleteAsync(user);
                TempData["Message"] = "User deleted.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoles(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return NotFound();

            var user = await _um.FindByIdAsync(id);
            if (user == null) return NotFound();

            var allRoles = await _rm.Roles.Select(r => r.Name).ToListAsync();
            var userRoles = await _um.GetRolesAsync(user);

            var model = new ManageRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = allRoles
            };

            ViewBag.UserRoles = userRoles;

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToRole(string userId, string role)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(role))
                return RedirectToAction("Index");

            var user = await _um.FindByIdAsync(userId);
            if (user != null && await _rm.RoleExistsAsync(role))
            {
                if (!await _um.IsInRoleAsync(user, role))
                {
                    await _um.AddToRoleAsync(user, role);
                    TempData["Message"] = $"Role '{role}' added to {user.UserName} successfully!";
                }
            }

            return RedirectToAction("ManageRoles", new { id = userId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromRole(string userId, string role)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(role))
                return RedirectToAction("Index");

            var user = await _um.FindByIdAsync(userId);
            if (user != null && await _rm.RoleExistsAsync(role))
            {
                if (await _um.IsInRoleAsync(user, role))
                {
                    await _um.RemoveFromRoleAsync(user, role);
                    TempData["Message"] = $"Role '{role}' removed from {user.UserName} successfully!";
                }
            }

            return RedirectToAction("ManageRoles", new { id = userId });
        }
    }
}