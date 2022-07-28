using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_310_W22SD_Assignment.Models;

namespace SD_310_W22SD_Assignment.Controllers
{
    public class SongListController : Controller
    {
        private MyTunesContext _context;

        public SongListController(MyTunesContext context)
        {
            _context = context;
        }
        
        public IActionResult Index(int? userId, bool error = false)
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

            ViewBag.UserId = userId;
            ViewBag.ErrorMessage = error ? "The user does not have enough money to buy the song" : null;
            ViewData["Users"] = _context.Users.ToList();
            ViewData["Songs"] = _context.Songs.Include(s => s.Purchases).Where(s => s.Purchases.FirstOrDefault(p => p.UserId == userId) == null).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Buy([Bind("UserId", "SongId")] Purchase purchase)
        {
            purchase.PurchasedAt = DateTime.Now;

            var user = _context.Users.First(u => u.Id == purchase.UserId);
            var song = _context.Songs.First(s => s.Id == purchase.SongId);

            if (user.Money < song.Price)
            {
                return RedirectToAction(nameof(Index), new { userId = purchase.UserId, error = true });
            }

            user.Money -= song.Price;

            _context.Update(user);
            _context.Add(purchase);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { userId = purchase.UserId });
        }
    }
}
