using Microsoft.AspNetCore.Identity;
using MvcMovie.Models;
using MvcComments.Models;
using System.Collections.Generic;
using MvcSeries.Models;

namespace IMDB.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Movies> Movies { get; set; }
        public List<Series> Series { get; set; }
        public List<Watchlist> Watchlist { get; set; }
        public List<Comments> Comments { get; set; }

    }
}
