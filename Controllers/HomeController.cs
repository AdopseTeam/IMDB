using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using IMDB.Models;
using MvcMovie.Data;
using MvcActor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace IMDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcMovieContext _movie;

        private readonly MvcActorContext _actor;

        public HomeController(ILogger<HomeController> logger, MvcMovieContext movie, MvcActorContext actor)
        {
            _logger = logger;
            _movie = movie;
            _actor = actor;
        }

        public IActionResult Index()
        {
            var movies = from m in _movie.Movies select m;
            var actors = from a in _actor.Actor select a;
            dynamic mymodel = new ExpandoObject();
            mymodel.Movie = movies;
            mymodel.Actor = actors;
            return View(mymodel);
        }

        public async Task<IActionResult> ReleasedOnThisMonth()
        {
            var movies = from m in _movie.Movies.Where(m => (m.ReleaseDate).Month == DateTime.Now.Month)
                         select m;

            return View(await movies.ToListAsync());
        }

        public async Task<IActionResult> TopMovies()
        {
            var movies = from m in _movie.Movies.Where(m => m.Rating >= 8) orderby m.Rating descending
                        select m;

            return View(await movies.ToListAsync());
        }

        public async Task<IActionResult> BornThisMonth()
        {
            var actors = from a in _actor.Actor.Where(a => (a.Birthday).Month == DateTime.Now.Month) orderby (a.Birthday).Day
                    select a;

            return View(await actors.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
