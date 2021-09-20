using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class Screening
    {
        public int Id { get; set; }
        public Show Movie { get; set; }
        public int MovieId { get; set; }
        public ICollection<User> Spectators { get; set; }
        public DateTime ScreeningTime { get; set; }
    }
}