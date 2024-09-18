using Microsoft.AspNetCore.Mvc;

namespace FeedTheFurballsMVC.Models
{
    public class AdminDashboardViewModel
    {
        public required List<string> Images { get; set; }
    }

}
