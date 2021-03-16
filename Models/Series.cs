using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MvcSeries.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int Seasons { get; set; }
        public string Genre { get; set; }
        public decimal Rating { get; set; }
    }
}