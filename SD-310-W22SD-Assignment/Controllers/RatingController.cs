using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_310_W22SD_Assignment.Models;

namespace SD_310_W22SD_Assignment.Controllers
{
    public class RatingController : Controller
    {
        private MyTunesContext _context;

        public RatingController(MyTunesContext context)
        {
            _context = context;
        }

        public IActionResult Index(int userId, int songId)
        {
            var rating = _context.Ratings.FirstOrDefault(r => r.UserId == userId && r.SongId == songId);

            ViewBag.UserId = userId;
            ViewBag.SongId = songId;
            ViewBag.Rating = rating;

            return View();
        }

        [HttpPost]
        public IActionResult Rate([Bind("UserId", "SongId", "Rating1")] Rating rating)
        {
            var existingRating = _context.Ratings.FirstOrDefault(r => r.UserId == rating.UserId && r.SongId == rating.SongId);

            if (existingRating != null)
            {
                existingRating.Rating1 = rating.Rating1;
                _context.Update(existingRating);
            }
            else
            {
                _context.Add(rating);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { userId = rating.UserId, songId = rating.SongId });
        }
    }
}
