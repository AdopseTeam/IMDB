using System;
using System.Linq;
using System.Threading.Tasks;
using IMDB.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using System.Dynamic;
using MvcActor.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IMDB.Controllers
{
    public class MoviesController : Controller
    {
        private const string ControllerName = "Movies";

        private readonly MvcMovieContext _context;
        private UserManager<IdentityUser> _userManager;
        private readonly MvcActorContext _actorContext;



        public MoviesController(MvcMovieContext context,UserManager<IdentityUser> userManager, MvcActorContext actorContext)
        {
            _context = context;
            _userManager = userManager;
            _actorContext = actorContext;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString,int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }

            var movies = from m in _context.Movies
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                string lowerMovie = searchString.ToLower();
                movies = movies.Where(s => s.Title.ToLower().Contains(lowerMovie));
            }

            int pageSize = 16;
            return View(await PaginatedList<Movies>.CreateAsync(movies.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var actorList = _context.Movies.FirstOrDefault(m => m.Id == id).Cast.Split(",").ToList();
            actorList.RemoveAt(actorList.Count - 1);
            var actors = actorList.Select(int.Parse).ToList();
            dynamic mymodel = new ExpandoObject();
            mymodel.Movie =  await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            mymodel.MComments = await _context.MComments.Where(s => s.MId == id).ToListAsync();
            var finalActors = new List<MvcActor.Models.Actor>();
            foreach(int actorId in actors){
                finalActors.Add(await _actorContext.Actor.FirstOrDefaultAsync(a => a.ActorId == actorId));
            }
            mymodel.Actor = finalActors;
            if (mymodel.Movie == null)
            {
                return NotFound();
            }

            return View(mymodel);
        }
        public LocalRedirectResult NewMComment(int Movieid, string text)
        {
            var user = _userManager.GetUserName(HttpContext.User);
            var comment = new MovieComment
            {
                Id =  _context.MComments.Max(s => s.Id) + 1,
                MId = (int)Movieid,
                Creator = user,
                Text = text
            };
            _context.MComments.Add(comment);
            _context.SaveChanges();
            return LocalRedirect("/Movies/Details/" + Movieid);
        }

        public LocalRedirectResult LikeMovie(int Movieid){
            var user = _userManager.GetUserName(HttpContext.User);
            var movie = _context.Movies.FirstOrDefault(m => m.Id == Movieid);
            movie.Votes = movie.Votes + 1;
            _context.SaveChanges();
            return LocalRedirect("/Movies/Details/" + Movieid);
        }
        public LocalRedirectResult DislikeMovie(int Movieid){
            var user = _userManager.GetUserName(HttpContext.User);
            var movie = _context.Movies.FirstOrDefault(m => m.Id == Movieid);
            movie.Votes = movie.Votes -1;
            _context.SaveChanges();
            return LocalRedirect("/Movies/Details/" + Movieid);
        }

    }
}
