using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BloodDonationManagementSystem.Interfaces;
using BloodDonationManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationManagementSystem.Controllers
{
    [Authorize(Policy = "PatientAccess")]
    public class PatientController : Controller
    {
        private readonly IPatientRepository _pr;
        private readonly SignInManager<User> _signInManager;

        public PatientController(IPatientRepository pr, SignInManager<User> signInManager)
        {
            _pr = pr;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RequestBlood()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestBlood(BloodRequest br)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Account/Patient/Login", new { area = "Identity" });
            }

            br.UserId = userId; // Populates foreign key
            br.PatientEmail = email;
            br.Status = "Pending";
            br.RequestDate = DateTime.Now;

            _pr.AddBloodRequest(br);

            TempData["AlertMessage"] = "Blood request submitted successfully!";
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public IActionResult MyRequests()
        {
            string email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;

            var requests = _pr.ViewBloodRequest(email) ?? new List<BloodRequest>();
            return View(requests);
        }

        [HttpGet]
        public IActionResult ViewMatchingDonors()
        {
            string email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;

            var request = _pr.GetApprovedRequest(email);

            if (request == null)
            {
                ViewBag.ApprovalStatus = "NotApproved";
                // Fixed: Return List<User> to match PatientRepository.ViewMatchingDonors return type
                return View(new List<User>());
            }

            ViewBag.ApprovalStatus = "Approved";
            var donors = _pr.ViewMatchingDonors(request.BloodGroup) ?? new List<User>();
            return View(donors);
        }

        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Proper ASP.NET Core Identity SignOut
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();

            return RedirectToPage("/Account/Patient/PatientLogin", new { area = "Identity" });
        }
    }
}