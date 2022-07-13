using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_310_W22SD_Assignment.Models;

namespace SD_310_W22SD_Assignment.Controllers
{
    public class SongController : Controller
    {
        private MyTunesContext _context;

        public SongController(MyTunesContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? artistId)
        {
            if (artistId == null)
            {
                var artist = _context.Artists.FirstOrDefault();
                if (artist == null)
                {
                    return NotFound("Artist not found");
                }
                else
                {
                    return RedirectToAction(nameof(Index), new { artistId = artist.Id });
                }
            }

            var songs = _context.Artists.Include(a => a.Songs).ThenInclude(s => s.Collections).First(a => a.Id == artistId).Songs.ToList();

            ViewData["Artists"] = _context.Artists.ToList();

            return View(songs);
        }
    }
}
