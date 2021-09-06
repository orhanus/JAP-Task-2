using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class Show
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverImageUrl { get; set; }
        public string ShowType { get; set; }
        public ICollection<Actor> Actors { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}