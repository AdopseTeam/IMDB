using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MvcActor.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}