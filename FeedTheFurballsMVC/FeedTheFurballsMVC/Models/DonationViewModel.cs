using Microsoft.AspNetCore.Mvc;

namespace FeedTheFurballsMVC.Models
{
    public class DonationViewModel
    {
        public decimal Goal { get; set; }
        public decimal TotalDonations { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Remaining => Goal - TotalDonations;
    }

}
