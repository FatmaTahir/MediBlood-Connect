using BloodDonationManagementSystem.Interfaces;
using BloodDonationManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationManagementSystem.Controllers
{
   [Authorize(Policy ="DonorAccess")]
    public class DonorController:Controller
    {
        private readonly IDonorRepository _dr;
        private readonly UserManager<User> _userManager; 

        public DonorController(IDonorRepository dr, UserManager<User> userManager)
        {
            _dr = dr;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult DonorDashboard()
        {
            string userId = _userManager.GetUserId(User); 

            var donor = _dr.GetDonorById(userId);  

            return View(donor);
        }


        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult EditProfile(User donor)
        {
            _dr.UpdateDonor(donor);
            return RedirectToAction("DonorDashboard");
        }

        [HttpGet]
        public IActionResult ProfileStatus()
        {
            string userId = _userManager.GetUserId(User);
            DonationRequest request = _dr.GetDonationRequest(userId);
            return View(request);
        }
        [HttpGet]
        public IActionResult RequestDonation()
        {
            string userId = _userManager.GetUserId(User);

            var donor = _dr.GetDonorById(userId);
            return View(donor);
        }
        [HttpPost]
        public IActionResult RequestDonation(DonationRequest request)
        {
            string userId = _userManager.GetUserId(User);
            request.Status = "pending";
            _dr.AddDonationRequest(request,userId);
            return RedirectToAction("DonorDashboard");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            return View("~/Views/Shared/HomePage.cshtml");
        }

    }
}
