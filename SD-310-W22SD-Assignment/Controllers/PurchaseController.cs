using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_310_W22SD_Assignment.Models;

namespace SD_310_W22SD_Assignment.Controllers
{
    public class PurchaseController : Controller
    {
        private MyTunesContext _context;

        public PurchaseController(MyTunesContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? userId)
        {
            if (userId == null)
            {
                var user = _context.Users.FirstOrDefault();
                if (user == null)
                {
                    return NotFound("User not found");
                }
                else
                {
                    return RedirectToAction(nameof(Index), new { userId = user.Id });
                }
            }

            var purchases = _context.Purchases.Include(p => p.Song).Where(p => p.UserId == userId).ToList();

            ViewBag.Users = _context.Users.ToList();

            return View(purchases);
        }

        [HttpPost]
        public IActionResult Refund(int userId, int songId)
        {
            var purchase = _context.Purchases.Include(p => p.User).Include(p => p.Song).First(p => p.UserId == userId && p.SongId == songId);
            var user = purchase.User;
            var song = purchase.Song;
            var rating = _context.Ratings.FirstOrDefault(r => r.UserId == userId && r.SongId == songId);

            user.Money += song.Price;

            _context.Update(user);
            _context.Remove(purchase);
            if (rating != null)
            {
                _context.Remove(rating);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { userId = userId });
        }
    }
}
