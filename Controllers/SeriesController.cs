using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSeries.Data;
using MvcSeries.Models;
using MvcActor.Data;
using System.Dynamic;
using System.Linq;
using IMDB.Views;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace IMDB.Controllers
{
    public class SeriesController : Controller
    {
        private const string ControllerName = "Series";
        private readonly MvcSeriesContext _context;
        private UserManager<IdentityUser> _userManager;
        private readonly MvcActorContext _actorContext;


        public SeriesController(MvcSeriesContext context, UserManager<IdentityUser> userManager, MvcActorContext actorContext)
        {
            _context = context;
            _userManager = userManager;
            _actorContext = actorContext;
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
            var actorList = _context.Series.FirstOrDefault(m => m.Id == id).Cast.Split(",").ToList();
            actorList.RemoveAt(actorList.Count - 1);
            var actors = actorList.Select(int.Parse).ToList();
            dynamic mymodel = new ExpandoObject();
            mymodel.SComments = await _context.SComments.Where(s => s.SId == id).ToListAsync();
            mymodel.Series = await _context.Series.FirstOrDefaultAsync(m => m.Id == id);
            var finalActors = new List<MvcActor.Models.Actor>();
            foreach(int actorId in actors){
                finalActors.Add(await _actorContext.Actor.FirstOrDefaultAsync(a => a.ActorId == actorId));
            }
            mymodel.Actor = finalActors;
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
        public LocalRedirectResult LikeSeries(int SeriesId){
            var user = _userManager.GetUserName(HttpContext.User);
            var series = _context.Series.FirstOrDefault(m => m.Id == SeriesId);
            if(series.Likes?.ToList() == null){
                var newList = new List<string>();
                newList.Add(user);
                series.Likes = newList;
                series.Votes = series.Votes + 1;
                _context.SaveChanges();
            }else if(!series.Likes.ToList().Contains(user)){
                var newList = series.Likes.ToList();
                newList.Add(user);
                series.Likes = newList;
                series.Votes = series.Votes + 1;
                _context.SaveChanges();
            }
            return LocalRedirect("/Series/Details/" + SeriesId);
        }
        public LocalRedirectResult DislikeSeries(int SeriesId){
            var user = _userManager.GetUserName(HttpContext.User);
            var series = _context.Series.FirstOrDefault(m => m.Id == SeriesId);
            if(series.Dislikes?.ToList() == null){
                var newList = new List<string>();
                newList.Add(user);
                series.Dislikes = newList;
                series.Votes = series.Votes + 1;
                _context.SaveChanges();
            }else if(!series.Dislikes.ToList().Contains(user)){
                var newList = series.Dislikes.ToList();
                newList.Add(user);
                series.Dislikes = newList;
                series.Votes = series.Votes + 1;
                _context.SaveChanges();
            }
            return LocalRedirect("/Series/Details/" + SeriesId);
        }
    }
}
