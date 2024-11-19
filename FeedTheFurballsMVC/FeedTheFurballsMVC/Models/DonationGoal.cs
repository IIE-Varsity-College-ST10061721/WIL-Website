using Microsoft.AspNetCore.Mvc;

namespace FeedTheFurballsMVC.Models
{
    public class DonationGoal
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal GoalAmount { get; set; }
        public decimal TotalDonations { get; set; }

        public decimal Remaining => GoalAmount - TotalDonations;
    }

}
