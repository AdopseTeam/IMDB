using IMDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using IMDB.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcMovie.Data;
using Npgsql;
using Microsoft.AspNetCore.Identity;

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
            _context.Watchlist.Add(watchlist);
            _context.SaveChanges();
        }

        public List<Watchlist> GetUserWatchlist(string userId)
        {
            return _context.Watchlist
                 .Include(w => w.Movies)
                 .Where(w => w.UserId.Equals(userId))
                 .ToList();
        }

        public void Remove(Watchlist watchlist)
        {
            _context.Watchlist.Remove(watchlist);
            _context.SaveChanges();
        }

        public Watchlist GetWatchlist(int Id)
        {
            return _context.Watchlist.FirstOrDefault(w => w.Id == Id);

        }

        public void AddUserIdToAppUserTable(string CurrentUserId)
        {
            var flag = _context.Set<IdentityUser>().Where(u => u.Id == CurrentUserId).FirstOrDefault();
            if (flag == null)
            {
                var User = new IdentityUser
                {
                    Id = CurrentUserId
                };
                _context.Add(User);
                _context.SaveChanges();
            }
        }
    }
}
