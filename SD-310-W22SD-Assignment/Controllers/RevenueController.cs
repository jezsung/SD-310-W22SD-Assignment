using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_310_W22SD_Assignment.Models;

namespace SD_310_W22SD_Assignment.Controllers
{
    public class RevenueController : Controller
    {
        private MyTunesContext _context;

        public RevenueController(MyTunesContext context)
        {
            _context = context;
        }

        public IActionResult Index(DateTime date)
        {
            var revenue = _context.Purchases.Include(p => p.Song).Where(p => p.PurchasedAt.Year == date.Year && p.PurchasedAt.Month == date.Month).Sum(p => p.Song.Price);
            ViewBag.Revenue = revenue;
            return View();
        }
    }
}
