using Azure.Core;
using BloodDonationManagementSystem.Interfaces;
using BloodDonationManagementSystem.Models;
using BloodDonationMangementSystem.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationMangementSystem.Controllers
{
    [Authorize(Policy ="AdminAccess")]
   
    public class AdminController:Controller
    {
        private readonly IAdminRepository _ar;
        private readonly IHubContext<NotificationHub> _hubContext;

        public AdminController(IAdminRepository ar, IHubContext<NotificationHub> hubContext)
        {
            _ar=ar;
            _hubContext = hubContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Dashboard()
        {
            AdminDashboardVM vm = new AdminDashboardVM
            {
                TotalDonoationRequests = _ar.GetTotalDonationRequests(),
                TotalBloodRequests = _ar.GetTotalBloodRequests(),
                
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View("~/Views/Shared/HomePage.cshtml");
        }
        [HttpGet]
        public IActionResult ViewBloodRequests(string bloodGroup="", string status="")
        {
            var requests = _ar.ViewBloodRequests(bloodGroup, status);
            return View(requests);
        }
        [HttpGet]
        public IActionResult BloodRequestsTable(string bloodGroup="", string status="")
        {
            var requests = _ar.ViewBloodRequests(bloodGroup, status);
            return PartialView("_BloodRequestsTable", requests); 
        }

        [HttpGet]
        public IActionResult ViewDonors(string bloodGroup = "", string availability = "")
        {
            var donors = _ar.ViewDonors(bloodGroup, availability);
            return View(donors);
        }
        [HttpGet]
        public IActionResult DonorsTable(string bloodGroup = "", string availability = "")
        {
            var donors = _ar.ViewDonors(bloodGroup, availability);
            return PartialView("_DonorsTable", donors);
        }

        [HttpGet]
        public IActionResult DonationRequests(string bloodGroup = "", string status = "")
        {
            var requests = _ar.ViewDonationRequests(bloodGroup, status);
            return View(requests); 
        }


        [HttpGet]
        public IActionResult DonationRequestsTable(string bloodGroup="", string status="")
        {
            var requests = _ar.ViewDonationRequests(bloodGroup, status);
            return PartialView("_DonationRequestsTable", requests);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveDonation(int requestId)
        {
            _ar.UpdateDonationRequestStatus(requestId);
             var donorId = _ar.GetDonorIdByRequest(requestId);
            if (!string.IsNullOrEmpty(donorId))
             {
                     await _hubContext.Clients.User(donorId)
                     .SendAsync("ReceiveNotification", "Your donation request has been approved!");
              }
         return RedirectToAction("DonationRequests");
        }
        [HttpPost]
        public async Task<IActionResult> RejectDonation(int requestId)
        {
            _ar.RejectDonation(requestId);
            var donorId = _ar.GetDonorIdByRequest(requestId);
            if (!string.IsNullOrEmpty(donorId))
            {
                await _hubContext.Clients.User(donorId)
                .SendAsync("ReceiveNotification", "Your donation request has been rejected!");
            }
            return RedirectToAction("DonationRequests");
        }
        [HttpPost]
        public async Task<IActionResult> ApproveBloodRequest(int requestId)
        {
            _ar.ApproveBloodRequest(requestId);
            var patientId = _ar.GetPatientIdByBloodRequest(requestId);
            if (!string.IsNullOrEmpty(patientId))
            {
                await _hubContext.Clients.User(patientId)
                    .SendAsync("ReceiveNotification", "Your blood request has been approved!");
            }

            return RedirectToAction("ViewBloodRequests");
        }
        [HttpPost]
        public async Task<IActionResult> RejectBloodRequest(int requestId)
        {
            _ar.RejectBloodRequest(requestId);
            var patientId = _ar.GetPatientIdByBloodRequest(requestId);
            if (!string.IsNullOrEmpty(patientId))
            {
                await _hubContext.Clients.User(patientId)
                    .SendAsync("ReceiveNotification", "Your blood request has been rejected!");
            }

            return RedirectToAction("ViewBloodRequests");
        }

        [HttpGet]
        public IActionResult ViewSpecificDonor(string id)
        {
            var donor= _ar.GetDonorById(id);
            return View(donor);
        }
        [HttpGet]
        public IActionResult ViewSpecificDonation(int id)
        {
            var donation = _ar.GetDonationById(id);
            return View(donation);
        }

        //[HttpGet]
        //public IActionResult DonationHistory()
        //{
        //    return View();
        //}
    }
}
