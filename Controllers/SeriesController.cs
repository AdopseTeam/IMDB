using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSeries.Data;
using MvcSeries.Models;
using System.Dynamic;
using System.Linq;
using IMDB.Views;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace IMDB.Controllers
{
    [Authorize]
    public class SeriesController : Controller
    {
        private const string ControllerName = "Series";
        private readonly MvcSeriesContext _context;
        private UserManager<IdentityUser> _userManager;


        public SeriesController(MvcSeriesContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Series
        public async Task<IActionResult> Index(string searchString,int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }

            var series = from s in _context.Series
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                string lowerSeries = searchString.ToLower();
                series = series.Where(s => s.Title.ToLower().Contains(lowerSeries));
            }

            int pageSize = 16;
            return View(await PaginatedList<Series>.CreateAsync(series.AsNoTracking(), pageNumber ?? 1, pageSize));
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
            var user = _userManager.GetUserName(HttpContext.User);
            var comment = new SeriesComment
            {
                Id =  _context.SComments.Max(s => s.Id) + 1,
                SId = (int)id,
                Creator = user,
                Text = text
            };
            _context.SComments.Add(comment);
            _context.SaveChanges();
            return LocalRedirect("/Series/Details/"+id);

        }
    }
}
