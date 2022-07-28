using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_310_W22SD_Assignment.Models;

namespace SD_310_W22SD_Assignment.Controllers
{
    public class TopController : Controller
    {
        private MyTunesContext _context;

        public TopController(MyTunesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var song = _context.Songs.Include(s => s.Purchases).Include(s => s.Artist).OrderByDescending(s => s.Purchases.Count).First();
            var artist = song.Artist;
            var top3RatedSongs = _context.Ratings.GroupBy(r => r.SongId).Select(g => new { g.Key, Rating = g.Sum(r => r.Rating1) }).OrderByDescending(e => e.Rating).Take(3).ToList();

            ViewData["TopSoldSong"] = song;
            ViewData["TopArtist"] = artist;
            ViewData["Top3RatedSongs"] = top3RatedSongs;

            return View();
        }
    }
}
