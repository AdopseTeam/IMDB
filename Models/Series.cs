using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using IMDB.Models;

namespace MvcSeries.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Poster_path { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Seasons { get; set; }
        public string Genre { get; set; }
        public decimal Rating { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public List<Watchlist> Watchlist { get; set; }
        public List<MvcComments.Models.Comments> Comments { get; set; }
        public List<MvcActor.Models.Actor> Actor {get; set;}
    }
}