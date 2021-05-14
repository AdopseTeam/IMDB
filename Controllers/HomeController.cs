using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using IMDB.Models;
using MvcMovie.Models;
using MvcMovie.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IMDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcMovieContext _released;
        public static DateTime today;
        public static int day, month;
        
        public HomeController(ILogger<HomeController> logger, MvcMovieContext released)
        {
            _logger = logger;
            _released = released;
        }
        
        public async Task<IActionResult> Index()
        {

                month = DateTime.Today.Month;
                today = DateTime.Today.Date;
                
                var movies = from m in _released.Movies
                    select m;
                
                movies = movies.Where(s => s.ReleaseDate.CompareTo(today)<0).Where(s => s.ReleaseDate.Month.Equals(month));
                
            return View(await movies.Take(12).ToListAsync());
        }
        
        /*
        public IActionResult Index()
        {
            return View();
        }
        */
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ReleasedOnThisMonth()
        {
                //day = DateTime.Today.Day;
                month = DateTime.Today.Month;
                today = DateTime.Today.Date;
                
                var movies = from m in _released.Movies
                        select m;
                
                //movies = movies.Where(s => s.ReleaseDate.Month.Equals(month));
                movies = movies.Where(s => s.ReleaseDate.CompareTo(today)<0).Where(s => s.ReleaseDate.Month.Equals(month));
                //movies = movies.Where(s => s.ReleaseDate.Month.Equals(month)).Where(s => s.ReleaseDate.Day.Equals(day));

            return View(await movies.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
