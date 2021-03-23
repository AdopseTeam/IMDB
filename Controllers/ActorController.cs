using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcActor.Data;

namespace IMDB.Controllers
{
    public class ActorController : Controller
    {
        private readonly MvcActorContext _context;

        public ActorController(MvcActorContext context)
        {
            _context = context;
        }

        // GET: Actor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actor.ToListAsync());
        }

        // GET: Actor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

    }
}
