using BloodDonationManagementSystem.Interfaces;
using BloodDonationManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationManagementSystem.Controllers
{
    [Authorize(Policy = "DonorAccess")]
    public class DonorController : Controller
    {
        private readonly IDonorRepository _dr;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public DonorController(
            IDonorRepository dr,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _dr = dr;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult DonorDashboard()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var donor = _dr.GetDonorById(userId);
            if (donor == null)
            {
                return NotFound("Donor profile not found.");
            }

            return View(donor);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(User donor)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Bind the logged-in user ID to prevent parameter tampering
            donor.Id = user.Id;

            if (!ModelState.IsValid)
            {
                return View(donor);
            }

            _dr.UpdateDonor(donor);
            return RedirectToAction(nameof(DonorDashboard));
        }

        [HttpGet]
        public IActionResult ProfileStatus()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var request = _dr.GetDonationRequest(userId);
            return View(request);
        }

        [HttpGet]
        public IActionResult RequestDonation()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var donor = _dr.GetDonorById(userId);
            if (donor == null)
            {
                return NotFound("Donor profile not found.");
            }

            return View(donor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestDonation(DonationRequest request)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            request.Status = "pending";
            request.RequestDate = DateTime.Now;
            _dr.AddDonationRequest(request, userId);

            return RedirectToAction(nameof(DonorDashboard));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}