using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Models
{
    public class Watchlist
    {
        public int Id { get; set; }
        public int movieId { get; set; }
        public string UserId { get; set; }
        public MvcMovie.Models.Movie Movie { get; set; }
        public ApplicationUser User {get;set;}
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    }
}
