using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Shows")]
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

        public double GetAverageRating()
        {
            double ratingsScore = 0;
            foreach(var rating in Ratings)
            {
                ratingsScore += rating.Score;
            }

            return Ratings.Count == 0 ? 0 : ratingsScore / Ratings.Count;
        }
    }
}