using BloodDonationManagementSystem.Interfaces;
using BloodDonationManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodDonationManagementSystem.Controllers
{
    [Authorize(Policy ="PatientAccess")]
    public class PatientController:Controller
    {
        private readonly IPatientRepository _pr;

        public PatientController(IPatientRepository pr)
        {
            _pr = pr;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult RequestBlood() {

            return View();
        }
        [HttpPost]
        public IActionResult RequestBlood(BloodRequest br)
        {
            br.PatientEmail = User.Identity.Name;
            br.Status = "Pending";
            _pr.AddBloodRequest(br);
            TempData["AlertMessage"] = "Blood request submitted successfully!";
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        public IActionResult MyRequests()
        {

             var email = User.Identity.Name;
             var requests = _pr.ViewBloodRequest(email);
             return View(requests);
        }

        [HttpGet]
        public IActionResult ViewMatchingDonors()
        {
            string email = User.Identity.Name;

            var request = _pr.GetApprovedRequest(email);

            if (request == null)
            {
                ViewBag.ApprovalStatus = "NotApproved";
                return View(new List<Donor>());
            }

            ViewBag.ApprovalStatus = "Approved";
            var donors = _pr.ViewMatchingDonors(request.BloodGroup);
            return View(donors);
        }


        [HttpGet]
        public IActionResult Logout()
        {
            return View("~/Views/Shared/HomePage.cshtml");
        }
    }
}
