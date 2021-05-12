using Microsoft.AspNetCore.Identity;
using System;

namespace IMDB.Models
{
    public class Watchlist
    {
        public int Id { get; set; }
        public int MoviesId { get; set; }
        public string UserId { get; set; }
        public MvcMovie.Models.Movies Movies { get; set; }
        public IdentityUser User {get;set;}
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
