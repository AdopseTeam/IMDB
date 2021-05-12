using System;
using System.ComponentModel.DataAnnotations;
using MvcMovie.Models;
using System.Collections.Generic;
using MvcSeries.Models;

namespace MvcActor.Models
{
    public class Actor
    {
        public int ActorId { get; set; }
        public string Bio { get; set; }
        public string Profile_pic_path { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public List<Movies> Movies {get; set;}
    }
}