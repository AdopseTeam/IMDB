using IMDB.Models;
using IMDB.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    [Authorize]

    public class WatchlistController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private IWatchlistRepo _watchlistRepo;

        public WatchlistController(IWatchlistRepo watchlistRepo,UserManager<IdentityUser> userManager)
        {
            _watchlistRepo = watchlistRepo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var watchlists = _watchlistRepo.GetUserWatchlist(userId);
            return View(watchlists);
        }

        public IActionResult NewMovie(int CurrentMovieId)
        {
            var CurrentUserId = _userManager.GetUserId(HttpContext.User);
            //_watchlistRepo.AddUserIdToAppUserTable(CurrentUserId);
            var watchlist = new Watchlist
            {
                MoviesId = CurrentMovieId,
                UserId= CurrentUserId
            };

            _watchlistRepo.Create(watchlist);


            return RedirectToAction(nameof(Index));
        }
        public IActionResult NewSeries(int CurrentSeriesId)
        {
            var CurrentUserId = _userManager.GetUserId(HttpContext.User);
            //_watchlistRepo.AddUserIdToAppUserTable(CurrentUserId);
            var watchlist = new Watchlist
            {
                MoviesId = CurrentSeriesId,
                UserId= CurrentUserId
            };

            _watchlistRepo.Create(watchlist);


            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int id)
        {

            var watchlist = _watchlistRepo.GetWatchlist(id);
            _watchlistRepo.Remove(watchlist);

            return RedirectToAction(nameof(Index));
        }
    }
}
