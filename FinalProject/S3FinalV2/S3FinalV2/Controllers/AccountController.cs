using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using S3FinalV2.Models;
using S3FinalV2.ViewModels;

namespace S3FinalV2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        public AccountController(UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm, IConfiguration config)
        {
            _userManager = um;
            _signInManager = sm;
            _config = config;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new ApplicationUser
            {
                UserName = vm.Username,
                Email = vm.Email,
                Role = _config.GetValue<string>("UserRole:DefaultRole") ?? "Customer",
                PasswordHash = "",
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
                return View(vm);
            }

            // sign in automatically
            await _signInManager.SignInAsync(user, isPersistent: vm.RememberMe);
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            var vm = new LoginViewModel { ReturnUrl = returnUrl };
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl)) return Redirect(vm.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Forgot password
        [HttpGet]
        public IActionResult ForgotPassword() => View(new ForgotPasswordViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user == null) // don't reveal that the user does not exist
            {
                TempData["Info"] = "If an account with that email exists, you will receive a reset link.";
                return RedirectToAction("Login");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetUrl = Url.Action("ResetPassword", "Account", new { token, email = vm.Email }, Request.Scheme);

            // In production send email with resetUrl. For test, display link
            TempData["ResetLink"] = resetUrl;
            TempData["Info"] = "Password reset link generated. (For demo the link is shown in Admin/Temp or TempData.)";
            return RedirectToAction("ForgotPasswordConfirmation");
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            ViewBag.ResetLink = TempData["ResetLink"];
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var vm = new ResetPasswordViewModel { Token = token, Email = email };
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user == null) return RedirectToAction("ResetPasswordConfirmation");

            var result = await _userManager.ResetPasswordAsync(user, vm.Token, vm.Password);
            if (result.Succeeded) return RedirectToAction("ResetPasswordConfirmation");

            foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
            return View(vm);
        }

        public IActionResult ResetPasswordConfirmation() => View();
        public IActionResult AccessDenied() => View();
    }
}