using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace FeedTheFurballsMVC.Models
{
    public class AdminDashboardViewModel
    {
        public required List<Gallery> Images { get; set; }
        public required List<DonationGoal> DonationGoals { get; set; }
    }

}
