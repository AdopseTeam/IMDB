using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MvcActor.Data;
using IMDB.Views;
using MvcActor.Models;
using System;

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
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            var actors = from a in _context.Actor
                         select a;

            int pageSize = 16;
            return View(await PaginatedList<Actor>.CreateAsync(actors.AsNoTracking(), pageNumber ?? 1, pageSize));
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
