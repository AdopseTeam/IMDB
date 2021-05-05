using IMDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using IMDB.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcMovie.Data;

namespace IMDB.Repo
{
    public class WatchlistRepo : IWatchlistRepo
    {
        private MvcMovieContext _context;

        public WatchlistRepo(MvcMovieContext context)
        {
            _context = context;
        }

        public void Create(Watchlist watchlist)
        {
            _context.Watchlists.Add(watchlist);
            _context.SaveChanges();
        }

        public List<Watchlist> GetUserWatchlist(string userId)
        {
           return _context.Watchlists
                .Include(w=>w.Movie)
                .Where(w=>w.UserId.Equals(userId))
                .ToList();
        }

        public void Remove(Watchlist watchlist)
        {
            _context.Watchlists.Remove(watchlist);
            _context.SaveChanges();
        }

        public Watchlist GetWatchlist(int Id)
        {
            return _context.Watchlists.FirstOrDefault(w => w.Id == Id);

        }


        public void AddUserIdToAppUserTable(string CurrentUserId)
        {
            var AppUser = new ApplicationUser
            {
                Id = CurrentUserId
            };

        }
    }
}
