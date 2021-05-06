using Microsoft.AspNetCore.Identity;
using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Movie> MoviesList { get; set; }
        public List<Watchlist> Watchlists { get; set; }
    }
}
