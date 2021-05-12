using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Repo
{
    public interface IWatchlistRepo
    {
        Watchlist GetWatchlist(int id);
        void Create(Watchlist watchlist);
        List<Watchlist> GetUserWatchlist(string userId);
        void Remove(Watchlist watchlist);
        //void AddUserIdToAppUserTable(string userId);
    }
}
