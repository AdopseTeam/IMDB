using Microsoft.AspNetCore.Identity;
using MvcMovie.Models;
using System.Collections.Generic;

namespace IMDB.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Movies> Movies { get; set; }
        public List<Watchlist> Watchlist { get; set; }
    }
}
