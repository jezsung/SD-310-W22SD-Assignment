using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_310_W22SD_Assignment.Models;

namespace SD_310_W22SD_Assignment.Controllers
{
    public class CollectionController : Controller
    {
        private MyTunesContext _context;

        public CollectionController(MyTunesContext context)
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

            ViewData["Users"] = _context.Users.ToList();

            var songs = _context.Collections.Where(c => c.UserId == userId).Include(c => c.Song).ThenInclude(s => s.Artist).Select(c => c.Song).OrderBy(s => s.Artist.Name).ToList();
            var songsByArtists = songs.GroupBy(s => s.Artist.Name).ToDictionary(g => g.Key, g => g.ToList());

            return View(songsByArtists);
        }

        public IActionResult Add()
        {
            ViewData["Users"] = _context.Users.ToList();
            ViewData["Songs"] = _context.Songs.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add([Bind("UserId", "SongId")] Collection collection)
        {
            var exists = _context.Collections.FirstOrDefault(c => c.SongId == collection.SongId && c.UserId == collection.UserId);
            if (exists != null)
            {
                return BadRequest("The user already has that song in the collection.");
            }


            _context.Add(collection);
            _context.SaveChanges();

            return RedirectToAction(nameof(Add));
        }
    }
}
