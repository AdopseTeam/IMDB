using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSeries.Data;
using MvcSeries.Models;
using System.Dynamic;
using System.Linq;


namespace IMDB.Controllers
{
    public class SeriesController : Controller
    {
        private const string ControllerName = "Series";
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
            mymodel.SComments = await _context.SComments.Where(s => s.SId == id).ToListAsync();

            mymodel.Series = await _context.Series.FirstOrDefaultAsync(m => m.Id == id);
            if (mymodel.Series == null)
            {
                return NotFound();
            }

            return View(mymodel);

        }

        public LocalRedirectResult NewComment(int id, string text)
        {
            var comment = new SeriesComment
            {
                Id =  _context.SComments.Max(s => s.Id) + 1,
                SId = (int)id,
                Creator = "Anonymous",
                Text = text
            };
            _context.SComments.Add(comment);
            _context.SaveChanges();
            return LocalRedirect("/Series/Details/"+id);

        }
    }
}
