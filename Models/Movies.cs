using IMDB.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models{
    public class Movies{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Poster_path { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Rating { get; set; }
        public decimal Budget { get; set; }
        public string Cast {get; set;}
        public int Votes {get; set;}
        public string Videokey { get; set; }

        public List<string> Likes {get;set;}
        public List<string> Dislikes {get; set;}

        public IdentityUser User { get; set; }
        public string UserId { get; set; }
        public List<Watchlist> Watchlist { get; set; }
    }

     public class MovieComment
    {
        public int Id { get; set; }

        public int MId { get; set; }
        public string Creator { get; set; }

        [DataType(DataType.Date)]
        public DateTime PubDate { get; set; } = DateTime.Now;
        public string Text { get; set; }
    }
}
