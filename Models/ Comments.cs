using System;
using System.ComponentModel.DataAnnotations;
using IMDB.Models;
using MvcMovie.Models;
using MvcSeries.Models;

namespace MvcComments.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Creator { get; set; }

        [DataType(DataType.Date)]
        public DateTime PubDate { get; set; }
        public string Text { get; set; }

        public Movies Movies {get; set;}
        public int MovieId{get; set;}
        public Series Series{get; set;}
        public int SeriesId{get; set;}
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

    }
}