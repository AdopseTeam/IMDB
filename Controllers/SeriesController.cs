using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSeries.Data;
using System.Dynamic;

namespace IMDB.Controllers
{
    public class SeriesController : Controller
    {
        private readonly MvcSeriesContext _context;

        public SeriesController(MvcSeriesContext context)
        {
            _context = context;
        }
        // GET: Series
        public async Task<IActionResult> Index()
        {
            return View(await _context.Series.ToListAsync());
        }

        // GET: Series/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            dynamic mymodel = new ExpandoObject();
            mymodel.Comments = await _context.Comments.FirstOrDefaultAsync(m => m.Id == id);
            mymodel.Series = await _context.Series.FirstOrDefaultAsync(m => m.Id == id);
            if (mymodel.Series == null)
            {
                return NotFound();
            }

            return View(mymodel);
        }
    }
}
