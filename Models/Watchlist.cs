using System;

namespace IMDB.Models
{
    public class Watchlist
    {
        public int Id { get; set; }
        public int movieId { get; set; }
        public string UserId { get; set; }
        public MvcMovie.Models.Movies Movies { get; set; }
        public ApplicationUser User {get;set;}
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
