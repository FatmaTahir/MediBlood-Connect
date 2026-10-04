using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BloodDonationMangementSystem.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
       
    }
}
